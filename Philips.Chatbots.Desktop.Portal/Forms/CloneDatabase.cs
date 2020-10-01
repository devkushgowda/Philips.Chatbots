using MongoDB.Driver;
using Philips.Chatbots.Database.Common;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Desktop.Portal.Data;
using System;
using System.Data;
using System.Linq;
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
                            await ToDb.Drop();
                        var fromBotConfiguration = await FromDb.BotCollection.FindOneById(MongoDbContext.BotAlphaName);
                        await ToDb.BotCollection.InsertManyAsync(await FromDb.BotCollection.Find(x => true).ToListAsync());

                        foreach (var profile in fromBotConfiguration?.Configuration?.ChatProfiles)
                        {
                            ToDb.SyncChatProfile(profile.Name);
                            await ToDb.ResourceCollection.InsertManyAsync(await FromDb.ResourceCollection.Find(x => true).ToListAsync());
                            await ToDb.LinkCollection.InsertManyAsync(await FromDb.LinkCollection.Find(x => true).ToListAsync());
                            await ToDb.ActionCollection.InsertManyAsync(await FromDb.ActionCollection.Find(x => true).ToListAsync());
                            await ToDb.TrainDataCollection.InsertManyAsync(await FromDb.TrainDataCollection.Find(x => true).ToListAsync());
                        }
                        DialogResult = DialogResult.OK;
                        cbxFromDb.Enabled = cbxToDb.Enabled = btnClone.Enabled = true;

                        MessageBox.Show("Operation completed without any error.", "Clone confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
    }
}
