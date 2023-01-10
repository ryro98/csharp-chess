namespace ChessFen
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbFEN = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.generateFenLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sourceSquareLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.targetSquareLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.clickedFigureLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.isOccupiedLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.colorPieceLabel = new System.Windows.Forms.Label();
            this.btnGenerateFen = new System.Windows.Forms.Button();
            this.generateFenTB = new System.Windows.Forms.TextBox();
            this.btnRotateBoard = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.turnLabel = new System.Windows.Forms.Label();
            this.movesLabel = new System.Windows.Forms.Label();
            this.panelNotation = new System.Windows.Forms.Panel();
            this.label123 = new System.Windows.Forms.Label();
            this.rowLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.columnLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tagLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.whiteENLabel = new System.Windows.Forms.Label();
            this.blackENLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.checkLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.attackedLabel = new System.Windows.Forms.Label();
            this.btnPuzzle = new System.Windows.Forms.Button();
            this.puzzleLabel = new System.Windows.Forms.Label();
            this.btnStartingPosition = new System.Windows.Forms.Button();
            this.panelDev = new System.Windows.Forms.Panel();
            this.rbUserMode = new System.Windows.Forms.RadioButton();
            this.rbDevMode = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.panelNotation.SuspendLayout();
            this.panelDev.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "FEN code";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 800);
            this.panel1.TabIndex = 1;
            // 
            // tbFEN
            // 
            this.tbFEN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.tbFEN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFEN.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbFEN.ForeColor = System.Drawing.Color.White;
            this.tbFEN.Location = new System.Drawing.Point(90, 6);
            this.tbFEN.Name = "tbFEN";
            this.tbFEN.Size = new System.Drawing.Size(422, 29);
            this.tbFEN.TabIndex = 2;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnGenerate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Location = new System.Drawing.Point(518, 6);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(94, 30);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // generateFenLabel
            // 
            this.generateFenLabel.AutoSize = true;
            this.generateFenLabel.Location = new System.Drawing.Point(618, 9);
            this.generateFenLabel.Name = "generateFenLabel";
            this.generateFenLabel.Size = new System.Drawing.Size(72, 20);
            this.generateFenLabel.TabIndex = 5;
            this.generateFenLabel.Text = "FEN label";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Source:";
            // 
            // sourceSquareLabel
            // 
            this.sourceSquareLabel.AutoSize = true;
            this.sourceSquareLabel.Location = new System.Drawing.Point(82, 10);
            this.sourceSquareLabel.Name = "sourceSquareLabel";
            this.sourceSquareLabel.Size = new System.Drawing.Size(42, 20);
            this.sourceSquareLabel.TabIndex = 7;
            this.sourceSquareLabel.Text = "label";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Target:";
            // 
            // targetSquareLabel
            // 
            this.targetSquareLabel.AutoSize = true;
            this.targetSquareLabel.Location = new System.Drawing.Point(82, 30);
            this.targetSquareLabel.Name = "targetSquareLabel";
            this.targetSquareLabel.Size = new System.Drawing.Size(42, 20);
            this.targetSquareLabel.TabIndex = 9;
            this.targetSquareLabel.Text = "label";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Figure:";
            // 
            // clickedFigureLabel
            // 
            this.clickedFigureLabel.AutoSize = true;
            this.clickedFigureLabel.Location = new System.Drawing.Point(254, 10);
            this.clickedFigureLabel.Name = "clickedFigureLabel";
            this.clickedFigureLabel.Size = new System.Drawing.Size(42, 20);
            this.clickedFigureLabel.TabIndex = 11;
            this.clickedFigureLabel.Text = "label";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(163, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "isOccupied:";
            // 
            // isOccupiedLabel
            // 
            this.isOccupiedLabel.AutoSize = true;
            this.isOccupiedLabel.Location = new System.Drawing.Point(254, 50);
            this.isOccupiedLabel.Name = "isOccupiedLabel";
            this.isOccupiedLabel.Size = new System.Drawing.Size(42, 20);
            this.isOccupiedLabel.TabIndex = 13;
            this.isOccupiedLabel.Text = "label";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(163, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Color:";
            // 
            // colorPieceLabel
            // 
            this.colorPieceLabel.AutoSize = true;
            this.colorPieceLabel.Location = new System.Drawing.Point(254, 30);
            this.colorPieceLabel.Name = "colorPieceLabel";
            this.colorPieceLabel.Size = new System.Drawing.Size(42, 20);
            this.colorPieceLabel.TabIndex = 15;
            this.colorPieceLabel.Text = "label";
            // 
            // btnGenerateFen
            // 
            this.btnGenerateFen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnGenerateFen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnGenerateFen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateFen.Location = new System.Drawing.Point(818, 854);
            this.btnGenerateFen.Name = "btnGenerateFen";
            this.btnGenerateFen.Size = new System.Drawing.Size(113, 30);
            this.btnGenerateFen.TabIndex = 17;
            this.btnGenerateFen.Text = "Generate FEN";
            this.btnGenerateFen.UseVisualStyleBackColor = false;
            this.btnGenerateFen.Click += new System.EventHandler(this.btnGenerateFen_Click);
            // 
            // generateFenTB
            // 
            this.generateFenTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.generateFenTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.generateFenTB.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.generateFenTB.ForeColor = System.Drawing.Color.White;
            this.generateFenTB.Location = new System.Drawing.Point(12, 854);
            this.generateFenTB.Name = "generateFenTB";
            this.generateFenTB.ReadOnly = true;
            this.generateFenTB.Size = new System.Drawing.Size(800, 29);
            this.generateFenTB.TabIndex = 18;
            // 
            // btnRotateBoard
            // 
            this.btnRotateBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnRotateBoard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnRotateBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRotateBoard.Location = new System.Drawing.Point(937, 853);
            this.btnRotateBoard.Name = "btnRotateBoard";
            this.btnRotateBoard.Size = new System.Drawing.Size(113, 30);
            this.btnRotateBoard.TabIndex = 19;
            this.btnRotateBoard.Text = "Rotate";
            this.btnRotateBoard.UseVisualStyleBackColor = false;
            this.btnRotateBoard.Click += new System.EventHandler(this.btnRotateBoard_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 20);
            this.label7.TabIndex = 20;
            this.label7.Text = "Turn:";
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.Location = new System.Drawing.Point(82, 119);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(42, 20);
            this.turnLabel.TabIndex = 21;
            this.turnLabel.Text = "label";
            // 
            // movesLabel
            // 
            this.movesLabel.AutoSize = true;
            this.movesLabel.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.movesLabel.Location = new System.Drawing.Point(8, 11);
            this.movesLabel.Name = "movesLabel";
            this.movesLabel.Size = new System.Drawing.Size(133, 67);
            this.movesLabel.TabIndex = 22;
            this.movesLabel.Text = "label";
            // 
            // panelNotation
            // 
            this.panelNotation.AutoScroll = true;
            this.panelNotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(36)))), ((int)(((byte)(33)))));
            this.panelNotation.Controls.Add(this.movesLabel);
            this.panelNotation.Location = new System.Drawing.Point(818, 307);
            this.panelNotation.Name = "panelNotation";
            this.panelNotation.Size = new System.Drawing.Size(502, 541);
            this.panelNotation.TabIndex = 23;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(13, 50);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(41, 20);
            this.label123.TabIndex = 24;
            this.label123.Text = "Row:";
            // 
            // rowLabel
            // 
            this.rowLabel.AutoSize = true;
            this.rowLabel.Location = new System.Drawing.Point(82, 50);
            this.rowLabel.Name = "rowLabel";
            this.rowLabel.Size = new System.Drawing.Size(42, 20);
            this.rowLabel.TabIndex = 25;
            this.rowLabel.Text = "label";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 20);
            this.label9.TabIndex = 26;
            this.label9.Text = "Column:";
            // 
            // columnLabel
            // 
            this.columnLabel.AutoSize = true;
            this.columnLabel.Location = new System.Drawing.Point(82, 70);
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(42, 20);
            this.columnLabel.TabIndex = 27;
            this.columnLabel.Text = "label";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 20);
            this.label8.TabIndex = 28;
            this.label8.Text = "Tag:";
            // 
            // tagLabel
            // 
            this.tagLabel.AutoSize = true;
            this.tagLabel.Location = new System.Drawing.Point(82, 90);
            this.tagLabel.Name = "tagLabel";
            this.tagLabel.Size = new System.Drawing.Size(42, 20);
            this.tagLabel.TabIndex = 29;
            this.tagLabel.Text = "label";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(163, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 20);
            this.label10.TabIndex = 30;
            this.label10.Text = "WhiteEN:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(163, 139);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 20);
            this.label11.TabIndex = 31;
            this.label11.Text = "BlackEN:";
            // 
            // whiteENLabel
            // 
            this.whiteENLabel.AutoSize = true;
            this.whiteENLabel.Location = new System.Drawing.Point(254, 119);
            this.whiteENLabel.Name = "whiteENLabel";
            this.whiteENLabel.Size = new System.Drawing.Size(42, 20);
            this.whiteENLabel.TabIndex = 32;
            this.whiteENLabel.Text = "label";
            // 
            // blackENLabel
            // 
            this.blackENLabel.AutoSize = true;
            this.blackENLabel.Location = new System.Drawing.Point(254, 139);
            this.blackENLabel.Name = "blackENLabel";
            this.blackENLabel.Size = new System.Drawing.Size(42, 20);
            this.blackENLabel.TabIndex = 33;
            this.blackENLabel.Text = "label";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 139);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 20);
            this.label12.TabIndex = 34;
            this.label12.Text = "Check:";
            // 
            // checkLabel
            // 
            this.checkLabel.AutoSize = true;
            this.checkLabel.Location = new System.Drawing.Point(82, 139);
            this.checkLabel.Name = "checkLabel";
            this.checkLabel.Size = new System.Drawing.Size(42, 20);
            this.checkLabel.TabIndex = 35;
            this.checkLabel.Text = "label";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(163, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 20);
            this.label13.TabIndex = 36;
            this.label13.Text = "Attacked:";
            // 
            // attackedLabel
            // 
            this.attackedLabel.AutoSize = true;
            this.attackedLabel.Location = new System.Drawing.Point(254, 70);
            this.attackedLabel.Name = "attackedLabel";
            this.attackedLabel.Size = new System.Drawing.Size(42, 20);
            this.attackedLabel.TabIndex = 37;
            this.attackedLabel.Text = "label";
            // 
            // btnPuzzle
            // 
            this.btnPuzzle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnPuzzle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnPuzzle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPuzzle.Location = new System.Drawing.Point(1192, 853);
            this.btnPuzzle.Name = "btnPuzzle";
            this.btnPuzzle.Size = new System.Drawing.Size(128, 30);
            this.btnPuzzle.TabIndex = 38;
            this.btnPuzzle.Text = "Random puzzle";
            this.btnPuzzle.UseVisualStyleBackColor = false;
            this.btnPuzzle.Click += new System.EventHandler(this.btnPuzzle_Click);
            // 
            // puzzleLabel
            // 
            this.puzzleLabel.AutoSize = true;
            this.puzzleLabel.Location = new System.Drawing.Point(982, 48);
            this.puzzleLabel.Name = "puzzleLabel";
            this.puzzleLabel.Size = new System.Drawing.Size(42, 20);
            this.puzzleLabel.TabIndex = 39;
            this.puzzleLabel.Text = "label";
            // 
            // btnStartingPosition
            // 
            this.btnStartingPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnStartingPosition.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.btnStartingPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartingPosition.Location = new System.Drawing.Point(1056, 853);
            this.btnStartingPosition.Name = "btnStartingPosition";
            this.btnStartingPosition.Size = new System.Drawing.Size(130, 30);
            this.btnStartingPosition.TabIndex = 40;
            this.btnStartingPosition.Text = "Starting position";
            this.btnStartingPosition.UseVisualStyleBackColor = false;
            this.btnStartingPosition.Click += new System.EventHandler(this.btnStartingPosition_Click);
            // 
            // panelDev
            // 
            this.panelDev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDev.Controls.Add(this.label2);
            this.panelDev.Controls.Add(this.sourceSquareLabel);
            this.panelDev.Controls.Add(this.label4);
            this.panelDev.Controls.Add(this.targetSquareLabel);
            this.panelDev.Controls.Add(this.attackedLabel);
            this.panelDev.Controls.Add(this.label3);
            this.panelDev.Controls.Add(this.label13);
            this.panelDev.Controls.Add(this.clickedFigureLabel);
            this.panelDev.Controls.Add(this.checkLabel);
            this.panelDev.Controls.Add(this.label5);
            this.panelDev.Controls.Add(this.label12);
            this.panelDev.Controls.Add(this.isOccupiedLabel);
            this.panelDev.Controls.Add(this.blackENLabel);
            this.panelDev.Controls.Add(this.label6);
            this.panelDev.Controls.Add(this.whiteENLabel);
            this.panelDev.Controls.Add(this.colorPieceLabel);
            this.panelDev.Controls.Add(this.label11);
            this.panelDev.Controls.Add(this.label7);
            this.panelDev.Controls.Add(this.label10);
            this.panelDev.Controls.Add(this.turnLabel);
            this.panelDev.Controls.Add(this.tagLabel);
            this.panelDev.Controls.Add(this.label123);
            this.panelDev.Controls.Add(this.label8);
            this.panelDev.Controls.Add(this.rowLabel);
            this.panelDev.Controls.Add(this.columnLabel);
            this.panelDev.Controls.Add(this.label9);
            this.panelDev.Location = new System.Drawing.Point(818, 119);
            this.panelDev.Name = "panelDev";
            this.panelDev.Size = new System.Drawing.Size(309, 169);
            this.panelDev.TabIndex = 41;
            this.panelDev.Visible = false;
            // 
            // rbUserMode
            // 
            this.rbUserMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbUserMode.BackColor = System.Drawing.Color.Red;
            this.rbUserMode.Checked = true;
            this.rbUserMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbUserMode.Location = new System.Drawing.Point(13, 4);
            this.rbUserMode.Name = "rbUserMode";
            this.rbUserMode.Size = new System.Drawing.Size(30, 30);
            this.rbUserMode.TabIndex = 42;
            this.rbUserMode.TabStop = true;
            this.rbUserMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbUserMode.UseVisualStyleBackColor = false;
            this.rbUserMode.CheckedChanged += new System.EventHandler(this.rbUserMode_CheckedChanged);
            // 
            // rbDevMode
            // 
            this.rbDevMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbDevMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbDevMode.Location = new System.Drawing.Point(43, 4);
            this.rbDevMode.Name = "rbDevMode";
            this.rbDevMode.Size = new System.Drawing.Size(30, 30);
            this.rbDevMode.TabIndex = 43;
            this.rbDevMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbDevMode.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rbUserMode);
            this.panel4.Controls.Add(this.rbDevMode);
            this.panel4.Location = new System.Drawing.Point(813, 71);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(78, 39);
            this.panel4.TabIndex = 44;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(818, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 20);
            this.label14.TabIndex = 45;
            this.label14.Text = "Dev Mode";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(21)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(1333, 892);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelDev);
            this.Controls.Add(this.btnStartingPosition);
            this.Controls.Add(this.puzzleLabel);
            this.Controls.Add(this.btnPuzzle);
            this.Controls.Add(this.panelNotation);
            this.Controls.Add(this.btnRotateBoard);
            this.Controls.Add(this.generateFenTB);
            this.Controls.Add(this.btnGenerateFen);
            this.Controls.Add(this.generateFenLabel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbFEN);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "FEN Chess Table";
            this.panelNotation.ResumeLayout(false);
            this.panelNotation.PerformLayout();
            this.panelDev.ResumeLayout(false);
            this.panelDev.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Panel panel1;
        private TextBox tbFEN;
        private Button btnGenerate;
        private Label generateFenLabel;
        private Label label2;
        private Label sourceSquareLabel;
        private Label label4;
        private Label targetSquareLabel;
        private Label label3;
        private Label clickedFigureLabel;
        private Label label5;
        private Label isOccupiedLabel;
        private Label label6;
        private Label colorPieceLabel;
        private Button btnGenerateFen;
        private TextBox generateFenTB;
        private Button btnRotateBoard;
        private Label label7;
        private Label turnLabel;
        private Label movesLabel;
        private Panel panelNotation;
        private Label label123;
        private Label rowLabel;
        private Label label9;
        private Label columnLabel;
        private Label label8;
        private Label tagLabel;
        private Label label10;
        private Label label11;
        private Label whiteENLabel;
        private Label blackENLabel;
        private Label label12;
        private Label checkLabel;
        private Label label13;
        private Label attackedLabel;
        private Button btnPuzzle;
        private Label puzzleLabel;
        private Button btnStartingPosition;
        private Panel panelDev;
        private RadioButton rbUserMode;
        private RadioButton rbDevMode;
        private Panel panel4;
        private Label label14;
    }
}