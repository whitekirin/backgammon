
namespace 五子棋_註冊_登錄
{
    partial class startGame
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
            this.database1DataSet = new 五子棋_註冊_登錄.Database1DataSet();
            this.database1DataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.database1DataSet1 = new 五子棋_註冊_登錄.Database1DataSet1();
            this.playerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.playerTableAdapter = new 五子棋_註冊_登錄.Database1DataSet1TableAdapters.playerTableAdapter();
            this.database1DataSet2 = new 五子棋_註冊_登錄.Database1DataSet2();
            this.playerBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.playerTableAdapter1 = new 五子棋_註冊_登錄.Database1DataSet2TableAdapters.playerTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // database1DataSet
            // 
            this.database1DataSet.DataSetName = "Database1DataSet";
            this.database1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // database1DataSetBindingSource
            // 
            this.database1DataSetBindingSource.DataSource = this.database1DataSet;
            this.database1DataSetBindingSource.Position = 0;
            // 
            // database1DataSet1
            // 
            this.database1DataSet1.DataSetName = "Database1DataSet1";
            this.database1DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // playerBindingSource
            // 
            this.playerBindingSource.DataMember = "player";
            this.playerBindingSource.DataSource = this.database1DataSet1;
            // 
            // playerTableAdapter
            // 
            this.playerTableAdapter.ClearBeforeFill = true;
            // 
            // database1DataSet2
            // 
            this.database1DataSet2.DataSetName = "Database1DataSet2";
            this.database1DataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // playerBindingSource1
            // 
            this.playerBindingSource1.DataMember = "player";
            this.playerBindingSource1.DataSource = this.database1DataSet2;
            // 
            // playerTableAdapter1
            // 
            this.playerTableAdapter1.ClearBeforeFill = true;
            // 
            // startGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "startGame";
            this.Text = "startGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.startGame_FormClosing);
            this.Load += new System.EventHandler(this.startGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource database1DataSetBindingSource;
        private Database1DataSet database1DataSet;
        private Database1DataSet1 database1DataSet1;
        private System.Windows.Forms.BindingSource playerBindingSource;
        private Database1DataSet1TableAdapters.playerTableAdapter playerTableAdapter;
        private Database1DataSet2 database1DataSet2;
        private System.Windows.Forms.BindingSource playerBindingSource1;
        private Database1DataSet2TableAdapters.playerTableAdapter playerTableAdapter1;
    }
}