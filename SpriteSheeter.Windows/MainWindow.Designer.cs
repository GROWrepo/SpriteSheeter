namespace SpriteSheeter
{
	partial class MainWindow
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.MenuStripMainMenu = new System.Windows.Forms.MenuStrip();
			this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemNewSpriteSheet = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSprite = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSpriteAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMainToolBar = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.SplitContainerEditor = new System.Windows.Forms.SplitContainer();
			this.ComboBoxMaximumSize = new System.Windows.Forms.ComboBox();
			this.ListViewSprites = new System.Windows.Forms.ListView();
			this.label1 = new System.Windows.Forms.Label();
			this.PanelPreview = new System.Windows.Forms.Panel();
			this.PictureBoxPreview = new System.Windows.Forms.PictureBox();
			this.OpenFileDialogSprite = new System.Windows.Forms.OpenFileDialog();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ToolTipSelectedDetails = new System.Windows.Forms.ToolTip(this.components);
			this.MenuStripMainMenu.SuspendLayout();
			this.ToolStripMainToolBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SplitContainerEditor)).BeginInit();
			this.SplitContainerEditor.Panel1.SuspendLayout();
			this.SplitContainerEditor.Panel2.SuspendLayout();
			this.SplitContainerEditor.SuspendLayout();
			this.PanelPreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// MenuStripMainMenu
			// 
			this.MenuStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemSprite});
			this.MenuStripMainMenu.Location = new System.Drawing.Point(0, 0);
			this.MenuStripMainMenu.Name = "MenuStripMainMenu";
			this.MenuStripMainMenu.Size = new System.Drawing.Size(1264, 24);
			this.MenuStripMainMenu.TabIndex = 0;
			this.MenuStripMainMenu.Text = "menuStrip1";
			// 
			// MenuItemFile
			// 
			this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemNewSpriteSheet,
            this.MenuItemOpen,
            this.toolStripMenuItem1,
            this.MenuItemSave,
            this.MenuItemSaveAs,
            this.toolStripMenuItem2,
            this.MenuItemExport,
            this.toolStripSeparator1,
            this.MenuItemExit});
			this.MenuItemFile.Name = "MenuItemFile";
			this.MenuItemFile.Size = new System.Drawing.Size(57, 20);
			this.MenuItemFile.Text = "파일(&F)";
			// 
			// MenuItemNewSpriteSheet
			// 
			this.MenuItemNewSpriteSheet.Name = "MenuItemNewSpriteSheet";
			this.MenuItemNewSpriteSheet.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.MenuItemNewSpriteSheet.Size = new System.Drawing.Size(268, 22);
			this.MenuItemNewSpriteSheet.Text = "새 스프라이트시트(&N)";
			this.MenuItemNewSpriteSheet.Click += new System.EventHandler(this.MenuItemNewSpriteSheet_Click);
			// 
			// MenuItemOpen
			// 
			this.MenuItemOpen.Name = "MenuItemOpen";
			this.MenuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.MenuItemOpen.Size = new System.Drawing.Size(268, 22);
			this.MenuItemOpen.Text = "열기(&O)";
			this.MenuItemOpen.Click += new System.EventHandler(this.MenuItemOpen_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(265, 6);
			// 
			// MenuItemSave
			// 
			this.MenuItemSave.Name = "MenuItemSave";
			this.MenuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.MenuItemSave.Size = new System.Drawing.Size(268, 22);
			this.MenuItemSave.Text = "저장(&S)";
			this.MenuItemSave.Click += new System.EventHandler(this.MenuItemSave_Click);
			// 
			// MenuItemSaveAs
			// 
			this.MenuItemSaveAs.Name = "MenuItemSaveAs";
			this.MenuItemSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.MenuItemSaveAs.Size = new System.Drawing.Size(268, 22);
			this.MenuItemSaveAs.Text = "다른 이름으로 저장(&A)";
			this.MenuItemSaveAs.Click += new System.EventHandler(this.MenuItemSaveAs_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(265, 6);
			// 
			// MenuItemExport
			// 
			this.MenuItemExport.Name = "MenuItemExport";
			this.MenuItemExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.MenuItemExport.Size = new System.Drawing.Size(268, 22);
			this.MenuItemExport.Text = "내보내기(&P)";
			this.MenuItemExport.Click += new System.EventHandler(this.MenuItemExport_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(265, 6);
			// 
			// MenuItemExit
			// 
			this.MenuItemExit.Name = "MenuItemExit";
			this.MenuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.MenuItemExit.Size = new System.Drawing.Size(268, 22);
			this.MenuItemExit.Text = "종료(&X)";
			this.MenuItemExit.Click += new System.EventHandler(this.MenuItemExit_Click);
			// 
			// MenuItemSprite
			// 
			this.MenuItemSprite.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSpriteAdd});
			this.MenuItemSprite.Name = "MenuItemSprite";
			this.MenuItemSprite.Size = new System.Drawing.Size(94, 20);
			this.MenuItemSprite.Text = "스프라이트(&S)";
			// 
			// MenuItemSpriteAdd
			// 
			this.MenuItemSpriteAdd.Name = "MenuItemSpriteAdd";
			this.MenuItemSpriteAdd.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
			this.MenuItemSpriteAdd.Size = new System.Drawing.Size(190, 22);
			this.MenuItemSpriteAdd.Text = "추가(&A)";
			this.MenuItemSpriteAdd.Click += new System.EventHandler(this.MenuItemSpriteAdd_Click);
			// 
			// ToolStripMainToolBar
			// 
			this.ToolStripMainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton4});
			this.ToolStripMainToolBar.Location = new System.Drawing.Point(0, 24);
			this.ToolStripMainToolBar.Name = "ToolStripMainToolBar";
			this.ToolStripMainToolBar.Size = new System.Drawing.Size(1264, 25);
			this.ToolStripMainToolBar.TabIndex = 1;
			this.ToolStripMainToolBar.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::SpriteSheeter.Properties.Resources.page;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "toolStripButton1";
			this.toolStripButton1.Click += new System.EventHandler(this.MenuItemNewSpriteSheet_Click);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = global::SpriteSheeter.Properties.Resources.folder;
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Text = "toolStripButton2";
			this.toolStripButton2.Click += new System.EventHandler(this.MenuItemOpen_Click);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = global::SpriteSheeter.Properties.Resources.disk;
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton3.Text = "toolStripButton3";
			this.toolStripButton3.Click += new System.EventHandler(this.MenuItemSave_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton4.Text = "toolStripButton4";
			// 
			// SplitContainerEditor
			// 
			this.SplitContainerEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainerEditor.Location = new System.Drawing.Point(0, 49);
			this.SplitContainerEditor.Name = "SplitContainerEditor";
			// 
			// SplitContainerEditor.Panel1
			// 
			this.SplitContainerEditor.Panel1.Controls.Add(this.ComboBoxMaximumSize);
			this.SplitContainerEditor.Panel1.Controls.Add(this.ListViewSprites);
			this.SplitContainerEditor.Panel1.Controls.Add(this.label1);
			this.SplitContainerEditor.Panel1MinSize = 128;
			// 
			// SplitContainerEditor.Panel2
			// 
			this.SplitContainerEditor.Panel2.Controls.Add(this.PanelPreview);
			this.SplitContainerEditor.Panel2MinSize = 128;
			this.SplitContainerEditor.Size = new System.Drawing.Size(1264, 632);
			this.SplitContainerEditor.SplitterDistance = 420;
			this.SplitContainerEditor.TabIndex = 2;
			// 
			// ComboBoxMaximumSize
			// 
			this.ComboBoxMaximumSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ComboBoxMaximumSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBoxMaximumSize.FormattingEnabled = true;
			this.ComboBoxMaximumSize.Items.AddRange(new object[] {
            "128",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192"});
			this.ComboBoxMaximumSize.Location = new System.Drawing.Point(68, 0);
			this.ComboBoxMaximumSize.Name = "ComboBoxMaximumSize";
			this.ComboBoxMaximumSize.Size = new System.Drawing.Size(351, 20);
			this.ComboBoxMaximumSize.TabIndex = 3;
			this.ComboBoxMaximumSize.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMaximumSize_SelectedIndexChanged);
			// 
			// ListViewSprites
			// 
			this.ListViewSprites.AllowDrop = true;
			this.ListViewSprites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ListViewSprites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.ListViewSprites.HideSelection = false;
			this.ListViewSprites.Location = new System.Drawing.Point(0, 26);
			this.ListViewSprites.Name = "ListViewSprites";
			this.ListViewSprites.Size = new System.Drawing.Size(419, 606);
			this.ListViewSprites.TabIndex = 2;
			this.ListViewSprites.UseCompatibleStateImageBehavior = false;
			this.ListViewSprites.View = System.Windows.Forms.View.Details;
			this.ListViewSprites.SelectedIndexChanged += new System.EventHandler(this.ListViewSprites_SelectedIndexChanged);
			this.ListViewSprites.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListViewSprites_DragDrop);
			this.ListViewSprites.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListViewSprites_DragEnter);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "최대 크기";
			// 
			// PanelPreview
			// 
			this.PanelPreview.AutoScroll = true;
			this.PanelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PanelPreview.Controls.Add(this.PictureBoxPreview);
			this.PanelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelPreview.Location = new System.Drawing.Point(0, 0);
			this.PanelPreview.Name = "PanelPreview";
			this.PanelPreview.Size = new System.Drawing.Size(840, 632);
			this.PanelPreview.TabIndex = 0;
			// 
			// PictureBoxPreview
			// 
			this.PictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PictureBoxPreview.Location = new System.Drawing.Point(0, 0);
			this.PictureBoxPreview.Name = "PictureBoxPreview";
			this.PictureBoxPreview.Size = new System.Drawing.Size(100, 50);
			this.PictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.PictureBoxPreview.TabIndex = 0;
			this.PictureBoxPreview.TabStop = false;
			this.PictureBoxPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxPreview_Paint);
			this.PictureBoxPreview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxPreview_MouseUp);
			// 
			// OpenFileDialogSprite
			// 
			this.OpenFileDialogSprite.Filter = "지원하는 모든 이미지 파일(*.png;*.bmp;*.jpg)|*.png;*.bmp;*.jpg";
			this.OpenFileDialogSprite.Multiselect = true;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "파일";
			this.columnHeader1.Width = 256;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 681);
			this.Controls.Add(this.SplitContainerEditor);
			this.Controls.Add(this.ToolStripMainToolBar);
			this.Controls.Add(this.MenuStripMainMenu);
			this.MainMenuStrip = this.MenuStripMainMenu;
			this.Name = "MainWindow";
			this.Text = "Sprite Sheeter";
			this.MenuStripMainMenu.ResumeLayout(false);
			this.MenuStripMainMenu.PerformLayout();
			this.ToolStripMainToolBar.ResumeLayout(false);
			this.ToolStripMainToolBar.PerformLayout();
			this.SplitContainerEditor.Panel1.ResumeLayout(false);
			this.SplitContainerEditor.Panel1.PerformLayout();
			this.SplitContainerEditor.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SplitContainerEditor)).EndInit();
			this.SplitContainerEditor.ResumeLayout(false);
			this.PanelPreview.ResumeLayout(false);
			this.PanelPreview.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxPreview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MenuStripMainMenu;
		private System.Windows.Forms.ToolStripMenuItem MenuItemFile;
		private System.Windows.Forms.ToolStripMenuItem MenuItemNewSpriteSheet;
		private System.Windows.Forms.ToolStripMenuItem MenuItemOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem MenuItemSave;
		private System.Windows.Forms.ToolStripMenuItem MenuItemSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem MenuItemExport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem MenuItemExit;
		private System.Windows.Forms.ToolStripMenuItem MenuItemSprite;
		private System.Windows.Forms.ToolStripMenuItem MenuItemSpriteAdd;
		private System.Windows.Forms.ToolStrip ToolStripMainToolBar;
		private System.Windows.Forms.SplitContainer SplitContainerEditor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView ListViewSprites;
		private System.Windows.Forms.Panel PanelPreview;
		private System.Windows.Forms.PictureBox PictureBoxPreview;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ToolStripButton toolStripButton3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButton4;
		private System.Windows.Forms.ComboBox ComboBoxMaximumSize;
		private System.Windows.Forms.OpenFileDialog OpenFileDialogSprite;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ToolTip ToolTipSelectedDetails;
	}
}

