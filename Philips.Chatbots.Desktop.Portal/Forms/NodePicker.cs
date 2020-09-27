using MongoDB.Driver;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Desktop.Portal.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Philips.Chatbots.Database.Common.DbAlias;
namespace Philips.Chatbots.Desktop.Portal
{
    public partial class NodePicker : Form
    {
        private string excludeChildId;
        private LinkType type;
        private List<ILinkInfo> linkList;
        public string NodeId => cbxNodes.SelectedValue?.ToString();
        public NodePicker(LinkType type, string excludeChildId, string titleText = "Select a node", string buttonText = "OK")
        {
            this.excludeChildId = excludeChildId;
            this.type = type;
            InitializeComponent();
            lblTitle.Text = titleText;
            btnOk.Text = buttonText;
        }



        private void InitializeData(List<ILinkInfo> linkList)
        {
            var bindingSource = new BindingSource();
            bindingSource.DataSource = linkList;
            cbxNodes.DataSource = bindingSource;
            cbxNodes.DisplayMember = nameof(ILinkInfo.Name);
            cbxNodes.ValueMember = nameof(ILinkInfo._id);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (cbxNodes.SelectedItem != null)
                this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cbxNodes_SelectedValueChanged(object sender, EventArgs e)
        {
            lblNodeId.Text = $"[id: {NodeId} ]";
        }

        private async void NodePicker_Load(object sender, EventArgs e)
        {
            linkList = await type.GetAllLinks();
            if (excludeChildId != null)
            {
                var node = await DbLinkCollection.FindOneById(excludeChildId);
                node?.CildrenRank.ForEach(child => linkList.Remove(linkList.FirstOrDefault(x => x._id == child.Key)));
            }
            InitializeData(linkList);
        }
    }
}
