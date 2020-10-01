using MongoDB.Driver;
using Philips.Chatbots.Database.Common;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Desktop.Portal.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class CloneDatabase : Form
    {
        private MongoDbContext FromDb;
        private MongoDbContext ToDb;
        public CloneDatabase()
        {
            InitializeComponent();
        }

        private async void btnClone_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbxFromDb.Text) && !string.IsNullOrWhiteSpace(cbxToDb.Text) && cbxFromDb.Text != cbxToDb.Text)
            {
                try
                {
                    if (DataProviders.ConfirmDialog($"Clone database '{cbxFromDb.Text}' to '{cbxToDb.Text}', with drop option {(chkbDrop.Checked ? "TRUE" : "FALSE")}"))
                    {
                        cbxFromDb.Enabled = cbxToDb.Enabled = btnClone.Enabled = false;
                        var config = Program.AppConfiguration;
                        FromDb = new MongoDbContext(connectionString: config.DbConnections[cbxFromDb.Text] ?? MongoDbContext.LocalConnection);
                        ToDb = new MongoDbContext(connectionString: config.DbConnections[cbxToDb.Text] ?? MongoDbContext.LocalConnection);
                        if (chkbDrop.Checked)
                        {
                            await ToDb.Drop();
                            if ((await FromDb.BotCollection.CountDocumentsAsync(x => true)) > 0)
                                await ToDb.BotCollection.InsertManyAsync(await FromDb.BotCollection.Find(x => true).ToListAsync());
                        }
                        var fromBotConfiguration = await FromDb.BotCollection.FindOneById(MongoDbContext.BotAlphaName);
                        if (chkbSelectProfile.Checked)
                        {
                            if (!chkbDrop.Checked)
                            {
                                var chatProfile = fromBotConfiguration.Configuration.ChatProfiles.FirstOrDefault(x => x.Name == cbxChatProfile.Text);
                                await ToDb.BotCollection.AddOrUpdateChatProfileById(MongoDbContext.BotAlphaName, chatProfile);
                                var toBotConfiguration = await ToDb.BotCollection.FindOneById(MongoDbContext.BotAlphaName);
                                var tempList = new List<KeyValuePair<string, string>>();
                                fromBotConfiguration.Configuration?.ResourceStrings?.ForEach(str =>
                                {
                                    var search = (KeyValuePair<string, string>)toBotConfiguration.Configuration?.ResourceStrings.FirstOrDefault(res => res.Key == str.Key);
                                    if (string.IsNullOrWhiteSpace(search.Key))
                                    {
                                        tempList.Add(str);
                                    }
                                });

                                if (tempList.Count > 0)
                                {
                                    await ToDb.BotCollection.AddStringResourceBatchById(MongoDbContext.BotAlphaName, tempList);
                                }
                            }

                            FromDb.SyncChatProfile(cbxChatProfile.Text);
                            ToDb.SyncChatProfile(cbxChatProfile.Text);
                            await ToDb.DropAllNodeCollections();
                            await CopyDb();
                        }
                        else
                        {
                            foreach (var profile in fromBotConfiguration?.Configuration?.ChatProfiles)
                            {
                                FromDb.SyncChatProfile(profile.Name);
                                ToDb.SyncChatProfile(profile.Name);
                                await CopyDb();
                            }
                        }
                        DialogResult = DialogResult.OK;
                        cbxFromDb.Enabled = cbxToDb.Enabled = btnClone.Enabled = true;

                        MessageBox.Show("Operation completed without any error.", "Clone confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (DialogResult == DialogResult.OK)
                        this.Close();
                }
            }
        }

        private async Task CopyDb()
        {
            if ((await FromDb.ResourceCollection.CountDocumentsAsync(x => true)) > 0)
                await ToDb.ResourceCollection.InsertManyAsync(await FromDb.ResourceCollection.Find(x => true).ToListAsync());
            if ((await FromDb.LinkCollection.CountDocumentsAsync(x => true)) > 0)
                await ToDb.LinkCollection.InsertManyAsync(await FromDb.LinkCollection.Find(x => true).ToListAsync());
            if ((await FromDb.ActionCollection.CountDocumentsAsync(x => true)) > 0)
                await ToDb.ActionCollection.InsertManyAsync(await FromDb.ActionCollection.Find(x => true).ToListAsync());
            if ((await FromDb.TrainDataCollection.CountDocumentsAsync(x => true)) > 0)
                await ToDb.TrainDataCollection.InsertManyAsync(await FromDb.TrainDataCollection.Find(x => true).ToListAsync());
        }

        private void ImportDatabase_Load(object sender, EventArgs e)
        {
            var config = Program.AppConfiguration;
            var items = config.DbConnections.Select(x => x.Key).ToArray();
            cbxFromDb.Items.AddRange(items);
            cbxToDb.Items.AddRange(items);
            if (items.Count() > 1)
            {
                cbxFromDb.Text = items[0];
                cbxToDb.Text = items[1];
            }
        }

        private async void chkbSelectProfile_CheckedChanged(object sender, EventArgs e)
        {
            cbxChatProfile.Items.Clear();
            if (chkbSelectProfile.Checked)
            {
                btnClone.Enabled = false;
                await LoadChatProfiles();
                btnClone.Enabled = true;
            }
            else
            {
                cbxChatProfile.Text = "";
                cbxChatProfile.Enabled = false;
                btnClone.Enabled = true;
                chkbDrop.Checked = true;
                chkbDrop.Enabled = false;
            }
        }

        private async Task LoadChatProfiles()
        {
            if (chkbSelectProfile.Checked)
            {
                cbxChatProfile.Items.Clear();

                chkbDrop.Enabled = true;
                chkbDrop.Checked = false;

                var config = Program.AppConfiguration;
                var selectedDbContext = new MongoDbContext(connectionString: config.DbConnections[cbxFromDb.Text] ?? MongoDbContext.LocalConnection);
                (await selectedDbContext.BotCollection.FindOneById(MongoDbContext.BotAlphaName))?.Configuration?.ChatProfiles?
                    .ForEach(
                    profile => cbxChatProfile.Items.Add(profile.Name));
                if (cbxChatProfile.Items.Count > 0)
                {
                    cbxChatProfile.Text = cbxChatProfile.Items[0].ToString();
                    btnClone.Enabled = true;
                    cbxChatProfile.Enabled = true;
                }
                else
                {
                    var notAvailable = "Not available";
                    cbxChatProfile.Items.Add(notAvailable);
                    cbxChatProfile.Text = notAvailable;
                    cbxChatProfile.Enabled = false;
                    btnClone.Enabled = false;
                }
            }
        }

        private async void cbxFromDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadChatProfiles();
        }
    }
}
