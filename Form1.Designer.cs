
namespace ZatcaIntegration
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
            this.btn_SimplifiedInvoice = new System.Windows.Forms.Button();
            this.btn_StandaredInvoice = new System.Windows.Forms.Button();
            this.btn_SimplifiedDebitNote = new System.Windows.Forms.Button();
            this.btn_StandaredDebitNote = new System.Windows.Forms.Button();
            this.btn_SimplifiedCreditNote = new System.Windows.Forms.Button();
            this.btn_StandardCreditNote = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_SimplifiedInvoice
            // 
            this.btn_SimplifiedInvoice.Location = new System.Drawing.Point(12, 12);
            this.btn_SimplifiedInvoice.Name = "btn_SimplifiedInvoice";
            this.btn_SimplifiedInvoice.Size = new System.Drawing.Size(365, 59);
            this.btn_SimplifiedInvoice.TabIndex = 0;
            this.btn_SimplifiedInvoice.Text = "GenerateXML_SimplifiedInvoice";
            this.btn_SimplifiedInvoice.UseVisualStyleBackColor = true;
            this.btn_SimplifiedInvoice.Click += new System.EventHandler(this.btn_SimplifiedInvoice_Click);
            // 
            // btn_StandaredInvoice
            // 
            this.btn_StandaredInvoice.Location = new System.Drawing.Point(12, 77);
            this.btn_StandaredInvoice.Name = "btn_StandaredInvoice";
            this.btn_StandaredInvoice.Size = new System.Drawing.Size(365, 57);
            this.btn_StandaredInvoice.TabIndex = 1;
            this.btn_StandaredInvoice.Text = "GenerateXML_StandaredInvoice";
            this.btn_StandaredInvoice.UseVisualStyleBackColor = true;
            this.btn_StandaredInvoice.Click += new System.EventHandler(this.btn_StandaredInvoice_Click);
            // 
            // btn_SimplifiedDebitNote
            // 
            this.btn_SimplifiedDebitNote.Location = new System.Drawing.Point(12, 140);
            this.btn_SimplifiedDebitNote.Name = "btn_SimplifiedDebitNote";
            this.btn_SimplifiedDebitNote.Size = new System.Drawing.Size(365, 57);
            this.btn_SimplifiedDebitNote.TabIndex = 2;
            this.btn_SimplifiedDebitNote.Text = "GenerateXML_SimplifiedDebitNote";
            this.btn_SimplifiedDebitNote.UseVisualStyleBackColor = true;
            this.btn_SimplifiedDebitNote.Click += new System.EventHandler(this.btn_SimplifiedDebitNote_Click);
            // 
            // btn_StandaredDebitNote
            // 
            this.btn_StandaredDebitNote.Location = new System.Drawing.Point(12, 203);
            this.btn_StandaredDebitNote.Name = "btn_StandaredDebitNote";
            this.btn_StandaredDebitNote.Size = new System.Drawing.Size(365, 57);
            this.btn_StandaredDebitNote.TabIndex = 3;
            this.btn_StandaredDebitNote.Text = "GenerateXML_StandaredDebitNote";
            this.btn_StandaredDebitNote.UseVisualStyleBackColor = true;
            this.btn_StandaredDebitNote.Click += new System.EventHandler(this.btn_StandaredDebitNote_Click);
            // 
            // btn_SimplifiedCreditNote
            // 
            this.btn_SimplifiedCreditNote.Location = new System.Drawing.Point(12, 266);
            this.btn_SimplifiedCreditNote.Name = "btn_SimplifiedCreditNote";
            this.btn_SimplifiedCreditNote.Size = new System.Drawing.Size(365, 57);
            this.btn_SimplifiedCreditNote.TabIndex = 4;
            this.btn_SimplifiedCreditNote.Text = "GenerateXML_SimplifiedCreditNote";
            this.btn_SimplifiedCreditNote.UseVisualStyleBackColor = true;
            this.btn_SimplifiedCreditNote.Click += new System.EventHandler(this.btn_SimplifiedCreditNote_Click);
            // 
            // btn_StandardCreditNote
            // 
            this.btn_StandardCreditNote.Location = new System.Drawing.Point(12, 329);
            this.btn_StandardCreditNote.Name = "btn_StandardCreditNote";
            this.btn_StandardCreditNote.Size = new System.Drawing.Size(365, 57);
            this.btn_StandardCreditNote.TabIndex = 5;
            this.btn_StandardCreditNote.Text = "GenerateXML_StandardCreditNote";
            this.btn_StandardCreditNote.UseVisualStyleBackColor = true;
            this.btn_StandardCreditNote.Click += new System.EventHandler(this.btn_StandardCreditNote_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 450);
            this.Controls.Add(this.btn_StandardCreditNote);
            this.Controls.Add(this.btn_SimplifiedCreditNote);
            this.Controls.Add(this.btn_StandaredDebitNote);
            this.Controls.Add(this.btn_SimplifiedDebitNote);
            this.Controls.Add(this.btn_StandaredInvoice);
            this.Controls.Add(this.btn_SimplifiedInvoice);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_SimplifiedInvoice;
        private System.Windows.Forms.Button btn_StandaredInvoice;
        private System.Windows.Forms.Button btn_SimplifiedDebitNote;
        private System.Windows.Forms.Button btn_StandaredDebitNote;
        private System.Windows.Forms.Button btn_SimplifiedCreditNote;
        private System.Windows.Forms.Button btn_StandardCreditNote;
    }
}

