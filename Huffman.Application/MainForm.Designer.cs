namespace Huffman.Application;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Button btnCompress;
    private System.Windows.Forms.Button btnDecompress;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Label lblTitle;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.btnCompress = new System.Windows.Forms.Button();
        this.btnDecompress = new System.Windows.Forms.Button();
        this.progressBar = new System.Windows.Forms.ProgressBar();
        this.lblStatus = new System.Windows.Forms.Label();
        this.lblTitle = new System.Windows.Forms.Label();
        this.SuspendLayout();

        // 
        // lblTitle
        // 
        this.lblTitle.AutoSize = false;
        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblTitle.Location = new System.Drawing.Point(0, 0);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(400, 60);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "Huffman Archiver";
        this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        // 
        // btnCompress
        // 
        this.btnCompress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.btnCompress.Location = new System.Drawing.Point(50, 80);
        this.btnCompress.Name = "btnCompress";
        this.btnCompress.Size = new System.Drawing.Size(140, 50);
        this.btnCompress.TabIndex = 1;
        this.btnCompress.Text = "Сжать";
        this.btnCompress.UseVisualStyleBackColor = true;
        this.btnCompress.Click += new System.EventHandler(this.BtnCompress_Click);

        // 
        // btnDecompress
        // 
        this.btnDecompress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.btnDecompress.Location = new System.Drawing.Point(210, 80);
        this.btnDecompress.Name = "btnDecompress";
        this.btnDecompress.Size = new System.Drawing.Size(140, 50);
        this.btnDecompress.TabIndex = 2;
        this.btnDecompress.Text = "Распаковать";
        this.btnDecompress.UseVisualStyleBackColor = true;
        this.btnDecompress.Click += new System.EventHandler(this.BtnDecompress_Click);

        // 
        // progressBar
        // 
        this.progressBar.Location = new System.Drawing.Point(50, 150);
        this.progressBar.Name = "progressBar";
        this.progressBar.Size = new System.Drawing.Size(300, 23);
        this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee; // Бегающая полоска
        this.progressBar.TabIndex = 3;
        this.progressBar.Visible = false; // Скрыт по умолчанию (State: Idle)

        // 
        // lblStatus
        // 
        this.lblStatus.AutoSize = false;
        this.lblStatus.Location = new System.Drawing.Point(50, 180);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(300, 23);
        this.lblStatus.TabIndex = 4;
        this.lblStatus.Text = "Готов к работе";
        this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 250);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.progressBar);
        this.Controls.Add(this.btnDecompress);
        this.Controls.Add(this.btnCompress);
        this.Controls.Add(this.lblTitle);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Huffman Archiver";
        this.ResumeLayout(false);
    }
}
