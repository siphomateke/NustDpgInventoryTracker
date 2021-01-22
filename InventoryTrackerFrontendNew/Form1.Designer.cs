
namespace InventoryTrackerFrontendNew
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.countryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.inventoryTrackerDataSet_Country = new InventoryTrackerFrontendNew.InventoryTrackerDataSet_Country();
            this.countryTableAdapter = new InventoryTrackerFrontendNew.InventoryTrackerDataSet_CountryTableAdapters.CountryTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.countryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryTrackerDataSet_Country)).BeginInit();
            this.SuspendLayout();
            // 
            // countryBindingSource
            // 
            this.countryBindingSource.DataMember = "Country";
            this.countryBindingSource.DataSource = this.inventoryTrackerDataSet_Country;
            this.countryBindingSource.CurrentChanged += new System.EventHandler(this.countryBindingSource_CurrentChanged);
            // 
            // inventoryTrackerDataSet_Country
            // 
            this.inventoryTrackerDataSet_Country.DataSetName = "InventoryTrackerDataSet_Country";
            this.inventoryTrackerDataSet_Country.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // countryTableAdapter
            // 
            this.countryTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 498);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.countryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryTrackerDataSet_Country)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private InventoryTrackerDataSet_Country inventoryTrackerDataSet_Country;
        private System.Windows.Forms.BindingSource countryBindingSource;
        private InventoryTrackerDataSet_CountryTableAdapters.CountryTableAdapter countryTableAdapter;
    }
}

