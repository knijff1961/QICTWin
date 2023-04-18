// <copyright file="frmLogging.Designer.cs" company="Fouroaks">
// Copyright (c) 2007, 2008 All Right Reserved, http://www.fouroaks.nl/
//
// This source is subject to the Microsoft Permissive License.
// Please see the License.txt file for more information.
// All other rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// </copyright>
// <author>Peter Knijff</author>
// <email>peter.knijff@fouroaks.nl</email>
// <date>2006-2010</date>
//
﻿namespace QICTWin
{
    partial class frmLogging
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkLogMessages1 = new System.Windows.Forms.CheckBox();
            this.chkLogMessages2 = new System.Windows.Forms.CheckBox();
            this.chkLogMessages3 = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkPrep_ParameterPositions = new System.Windows.Forms.CheckBox();
            this.chkPrep_UnusedCounts = new System.Windows.Forms.CheckBox();
            this.chkPrep_DisplayAllPairs = new System.Windows.Forms.CheckBox();
            this.chkPrep_DisplayAllPairs2 = new System.Windows.Forms.CheckBox();
            this.chkPrepare = new System.Windows.Forms.CheckBox();
            this.chkPrep_parameterValues = new System.Windows.Forms.CheckBox();
            this.chkPrep_InternalParameterValues = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkCore_CandidateNumber = new System.Windows.Forms.CheckBox();
            this.chkCore_UpdateSteps = new System.Windows.Forms.CheckBox();
            this.chkCore_AddCandidate = new System.Windows.Forms.CheckBox();
            this.chkCore_AllCandidateTestsets = new System.Windows.Forms.CheckBox();
            this.chkCore_ExaminePairs = new System.Windows.Forms.CheckBox();
            this.chkCore_BestValue = new System.Windows.Forms.CheckBox();
            this.chkCore_PlacedParams = new System.Windows.Forms.CheckBox();
            this.chkCore_PossibleValues = new System.Windows.Forms.CheckBox();
            this.chkCore_BestPair = new System.Windows.Forms.CheckBox();
            this.chkCore_BestPairPosition = new System.Windows.Forms.CheckBox();
            this.chkCore_BestPairOrder = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.chkFinal = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.chkConsole = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(241, 286);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkLogMessages1);
            this.tabPage1.Controls.Add(this.chkLogMessages2);
            this.tabPage1.Controls.Add(this.chkLogMessages3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(233, 260);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkLogMessages1
            // 
            this.chkLogMessages1.AccessibleName = "chkLogMessages1";
            this.chkLogMessages1.AutoSize = true;
            this.chkLogMessages1.Location = new System.Drawing.Point(6, 6);
            this.chkLogMessages1.Name = "chkLogMessages1";
            this.chkLogMessages1.Size = new System.Drawing.Size(111, 17);
            this.chkLogMessages1.TabIndex = 2;
            this.chkLogMessages1.Text = "Level 1 messages";
            this.chkLogMessages1.UseVisualStyleBackColor = true;
            // 
            // chkLogMessages2
            // 
            this.chkLogMessages2.AccessibleName = "chkLogMessages2";
            this.chkLogMessages2.AutoSize = true;
            this.chkLogMessages2.Location = new System.Drawing.Point(6, 29);
            this.chkLogMessages2.Name = "chkLogMessages2";
            this.chkLogMessages2.Size = new System.Drawing.Size(111, 17);
            this.chkLogMessages2.TabIndex = 1;
            this.chkLogMessages2.Text = "Level 2 messages";
            this.chkLogMessages2.UseVisualStyleBackColor = true;
            // 
            // chkLogMessages3
            // 
            this.chkLogMessages3.AccessibleName = "chkLogMessages3";
            this.chkLogMessages3.AutoSize = true;
            this.chkLogMessages3.Location = new System.Drawing.Point(6, 52);
            this.chkLogMessages3.Name = "chkLogMessages3";
            this.chkLogMessages3.Size = new System.Drawing.Size(111, 17);
            this.chkLogMessages3.TabIndex = 0;
            this.chkLogMessages3.Text = "Level 3 messages";
            this.chkLogMessages3.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkPrep_ParameterPositions);
            this.tabPage2.Controls.Add(this.chkPrep_UnusedCounts);
            this.tabPage2.Controls.Add(this.chkPrep_DisplayAllPairs);
            this.tabPage2.Controls.Add(this.chkPrep_DisplayAllPairs2);
            this.tabPage2.Controls.Add(this.chkPrepare);
            this.tabPage2.Controls.Add(this.chkPrep_parameterValues);
            this.tabPage2.Controls.Add(this.chkPrep_InternalParameterValues);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(233, 260);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Prepare";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkPrep_ParameterPositions
            // 
            this.chkPrep_ParameterPositions.AccessibleName = "chkPrep_ParameterPositions";
            this.chkPrep_ParameterPositions.AutoSize = true;
            this.chkPrep_ParameterPositions.Location = new System.Drawing.Point(6, 121);
            this.chkPrep_ParameterPositions.Name = "chkPrep_ParameterPositions";
            this.chkPrep_ParameterPositions.Size = new System.Drawing.Size(118, 17);
            this.chkPrep_ParameterPositions.TabIndex = 9;
            this.chkPrep_ParameterPositions.Text = "Parameter positions";
            this.chkPrep_ParameterPositions.UseVisualStyleBackColor = true;
            // 
            // chkPrep_UnusedCounts
            // 
            this.chkPrep_UnusedCounts.AccessibleName = "chkPrep_UnusedCounts";
            this.chkPrep_UnusedCounts.AutoSize = true;
            this.chkPrep_UnusedCounts.Location = new System.Drawing.Point(6, 144);
            this.chkPrep_UnusedCounts.Name = "chkPrep_UnusedCounts";
            this.chkPrep_UnusedCounts.Size = new System.Drawing.Size(98, 17);
            this.chkPrep_UnusedCounts.TabIndex = 8;
            this.chkPrep_UnusedCounts.Text = "Unused counts";
            this.chkPrep_UnusedCounts.UseVisualStyleBackColor = true;
            // 
            // chkPrep_DisplayAllPairs
            // 
            this.chkPrep_DisplayAllPairs.AccessibleName = "chkPrep_DisplayAllPairs";
            this.chkPrep_DisplayAllPairs.AutoSize = true;
            this.chkPrep_DisplayAllPairs.Location = new System.Drawing.Point(6, 75);
            this.chkPrep_DisplayAllPairs.Name = "chkPrep_DisplayAllPairs";
            this.chkPrep_DisplayAllPairs.Size = new System.Drawing.Size(98, 17);
            this.chkPrep_DisplayAllPairs.TabIndex = 7;
            this.chkPrep_DisplayAllPairs.Text = "Display all pairs";
            this.chkPrep_DisplayAllPairs.UseVisualStyleBackColor = true;
            // 
            // chkPrep_DisplayAllPairs2
            // 
            this.chkPrep_DisplayAllPairs2.AccessibleName = "chkPrep_DisplayAllPairs2";
            this.chkPrep_DisplayAllPairs2.AutoSize = true;
            this.chkPrep_DisplayAllPairs2.Location = new System.Drawing.Point(6, 98);
            this.chkPrep_DisplayAllPairs2.Name = "chkPrep_DisplayAllPairs2";
            this.chkPrep_DisplayAllPairs2.Size = new System.Drawing.Size(107, 17);
            this.chkPrep_DisplayAllPairs2.TabIndex = 6;
            this.chkPrep_DisplayAllPairs2.Text = "Display all pairs 2";
            this.chkPrep_DisplayAllPairs2.UseVisualStyleBackColor = true;
            // 
            // chkPrepare
            // 
            this.chkPrepare.AccessibleName = "chkPrepare";
            this.chkPrepare.AutoSize = true;
            this.chkPrepare.Location = new System.Drawing.Point(6, 6);
            this.chkPrepare.Name = "chkPrepare";
            this.chkPrepare.Size = new System.Drawing.Size(63, 17);
            this.chkPrepare.TabIndex = 5;
            this.chkPrepare.Text = "General";
            this.chkPrepare.UseVisualStyleBackColor = true;
            // 
            // chkPrep_parameterValues
            // 
            this.chkPrep_parameterValues.AccessibleName = "chkPrep_parameterValues";
            this.chkPrep_parameterValues.AutoSize = true;
            this.chkPrep_parameterValues.Location = new System.Drawing.Point(6, 29);
            this.chkPrep_parameterValues.Name = "chkPrep_parameterValues";
            this.chkPrep_parameterValues.Size = new System.Drawing.Size(108, 17);
            this.chkPrep_parameterValues.TabIndex = 4;
            this.chkPrep_parameterValues.Text = "Parameter values";
            this.chkPrep_parameterValues.UseVisualStyleBackColor = true;
            // 
            // chkPrep_InternalParameterValues
            // 
            this.chkPrep_InternalParameterValues.AccessibleName = "chkPrep_InternalParameterValues";
            this.chkPrep_InternalParameterValues.AutoSize = true;
            this.chkPrep_InternalParameterValues.Location = new System.Drawing.Point(6, 52);
            this.chkPrep_InternalParameterValues.Name = "chkPrep_InternalParameterValues";
            this.chkPrep_InternalParameterValues.Size = new System.Drawing.Size(145, 17);
            this.chkPrep_InternalParameterValues.TabIndex = 3;
            this.chkPrep_InternalParameterValues.Text = "Internal parameter values";
            this.chkPrep_InternalParameterValues.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chkCore_CandidateNumber);
            this.tabPage3.Controls.Add(this.chkCore_UpdateSteps);
            this.tabPage3.Controls.Add(this.chkCore_AddCandidate);
            this.tabPage3.Controls.Add(this.chkCore_AllCandidateTestsets);
            this.tabPage3.Controls.Add(this.chkCore_ExaminePairs);
            this.tabPage3.Controls.Add(this.chkCore_BestValue);
            this.tabPage3.Controls.Add(this.chkCore_PlacedParams);
            this.tabPage3.Controls.Add(this.chkCore_PossibleValues);
            this.tabPage3.Controls.Add(this.chkCore_BestPair);
            this.tabPage3.Controls.Add(this.chkCore_BestPairPosition);
            this.tabPage3.Controls.Add(this.chkCore_BestPairOrder);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(233, 260);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Core";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chkCore_CandidateNumber
            // 
            this.chkCore_CandidateNumber.AccessibleName = "chkCore_CandidateNumber";
            this.chkCore_CandidateNumber.AutoSize = true;
            this.chkCore_CandidateNumber.Location = new System.Drawing.Point(6, 213);
            this.chkCore_CandidateNumber.Name = "chkCore_CandidateNumber";
            this.chkCore_CandidateNumber.Size = new System.Drawing.Size(112, 17);
            this.chkCore_CandidateNumber.TabIndex = 20;
            this.chkCore_CandidateNumber.Text = "Candidate number";
            this.chkCore_CandidateNumber.UseVisualStyleBackColor = true;
            // 
            // chkCore_UpdateSteps
            // 
            this.chkCore_UpdateSteps.AccessibleName = "chkCore_UpdateSteps";
            this.chkCore_UpdateSteps.AutoSize = true;
            this.chkCore_UpdateSteps.Location = new System.Drawing.Point(6, 236);
            this.chkCore_UpdateSteps.Name = "chkCore_UpdateSteps";
            this.chkCore_UpdateSteps.Size = new System.Drawing.Size(89, 17);
            this.chkCore_UpdateSteps.TabIndex = 19;
            this.chkCore_UpdateSteps.Text = "Update steps";
            this.chkCore_UpdateSteps.UseVisualStyleBackColor = true;
            // 
            // chkCore_AddCandidate
            // 
            this.chkCore_AddCandidate.AccessibleName = "chkCore_AddCandidate";
            this.chkCore_AddCandidate.AutoSize = true;
            this.chkCore_AddCandidate.Location = new System.Drawing.Point(6, 167);
            this.chkCore_AddCandidate.Name = "chkCore_AddCandidate";
            this.chkCore_AddCandidate.Size = new System.Drawing.Size(95, 17);
            this.chkCore_AddCandidate.TabIndex = 18;
            this.chkCore_AddCandidate.Text = "Add candidate";
            this.chkCore_AddCandidate.UseVisualStyleBackColor = true;
            // 
            // chkCore_AllCandidateTestsets
            // 
            this.chkCore_AllCandidateTestsets.AccessibleName = "chkCore_AllCandidateTestsets";
            this.chkCore_AllCandidateTestsets.AutoSize = true;
            this.chkCore_AllCandidateTestsets.Location = new System.Drawing.Point(6, 190);
            this.chkCore_AllCandidateTestsets.Name = "chkCore_AllCandidateTestsets";
            this.chkCore_AllCandidateTestsets.Size = new System.Drawing.Size(126, 17);
            this.chkCore_AllCandidateTestsets.TabIndex = 17;
            this.chkCore_AllCandidateTestsets.Text = "All candidate testsets";
            this.chkCore_AllCandidateTestsets.UseVisualStyleBackColor = true;
            // 
            // chkCore_ExaminePairs
            // 
            this.chkCore_ExaminePairs.AccessibleName = "chkCore_ExaminePairs";
            this.chkCore_ExaminePairs.AutoSize = true;
            this.chkCore_ExaminePairs.Location = new System.Drawing.Point(6, 121);
            this.chkCore_ExaminePairs.Name = "chkCore_ExaminePairs";
            this.chkCore_ExaminePairs.Size = new System.Drawing.Size(91, 17);
            this.chkCore_ExaminePairs.TabIndex = 16;
            this.chkCore_ExaminePairs.Text = "Examine pairs";
            this.chkCore_ExaminePairs.UseVisualStyleBackColor = true;
            // 
            // chkCore_BestValue
            // 
            this.chkCore_BestValue.AccessibleName = "chkCore_BestValue";
            this.chkCore_BestValue.AutoSize = true;
            this.chkCore_BestValue.Location = new System.Drawing.Point(6, 144);
            this.chkCore_BestValue.Name = "chkCore_BestValue";
            this.chkCore_BestValue.Size = new System.Drawing.Size(76, 17);
            this.chkCore_BestValue.TabIndex = 15;
            this.chkCore_BestValue.Text = "Best value";
            this.chkCore_BestValue.UseVisualStyleBackColor = true;
            // 
            // chkCore_PlacedParams
            // 
            this.chkCore_PlacedParams.AccessibleName = "chkCore_PlacedParams";
            this.chkCore_PlacedParams.AutoSize = true;
            this.chkCore_PlacedParams.Location = new System.Drawing.Point(6, 75);
            this.chkCore_PlacedParams.Name = "chkCore_PlacedParams";
            this.chkCore_PlacedParams.Size = new System.Drawing.Size(114, 17);
            this.chkCore_PlacedParams.TabIndex = 14;
            this.chkCore_PlacedParams.Text = "Placed parameters";
            this.chkCore_PlacedParams.UseVisualStyleBackColor = true;
            // 
            // chkCore_PossibleValues
            // 
            this.chkCore_PossibleValues.AccessibleName = "chkCore_PossibleValues";
            this.chkCore_PossibleValues.AutoSize = true;
            this.chkCore_PossibleValues.Location = new System.Drawing.Point(6, 98);
            this.chkCore_PossibleValues.Name = "chkCore_PossibleValues";
            this.chkCore_PossibleValues.Size = new System.Drawing.Size(99, 17);
            this.chkCore_PossibleValues.TabIndex = 13;
            this.chkCore_PossibleValues.Text = "Possible values";
            this.chkCore_PossibleValues.UseVisualStyleBackColor = true;
            // 
            // chkCore_BestPair
            // 
            this.chkCore_BestPair.AccessibleName = "chkCore_BestPair";
            this.chkCore_BestPair.AutoSize = true;
            this.chkCore_BestPair.Location = new System.Drawing.Point(6, 6);
            this.chkCore_BestPair.Name = "chkCore_BestPair";
            this.chkCore_BestPair.Size = new System.Drawing.Size(117, 17);
            this.chkCore_BestPair.TabIndex = 12;
            this.chkCore_BestPair.Text = "Best pair estimation";
            this.chkCore_BestPair.UseVisualStyleBackColor = true;
            // 
            // chkCore_BestPairPosition
            // 
            this.chkCore_BestPairPosition.AccessibleName = "chkCore_BestPairPosition";
            this.chkCore_BestPairPosition.AutoSize = true;
            this.chkCore_BestPairPosition.Location = new System.Drawing.Point(6, 29);
            this.chkCore_BestPairPosition.Name = "chkCore_BestPairPosition";
            this.chkCore_BestPairPosition.Size = new System.Drawing.Size(106, 17);
            this.chkCore_BestPairPosition.TabIndex = 11;
            this.chkCore_BestPairPosition.Text = "Best pair position";
            this.chkCore_BestPairPosition.UseVisualStyleBackColor = true;
            // 
            // chkCore_BestPairOrder
            // 
            this.chkCore_BestPairOrder.AccessibleName = "chkCore_BestPairOrder";
            this.chkCore_BestPairOrder.AutoSize = true;
            this.chkCore_BestPairOrder.Location = new System.Drawing.Point(6, 52);
            this.chkCore_BestPairOrder.Name = "chkCore_BestPairOrder";
            this.chkCore_BestPairOrder.Size = new System.Drawing.Size(94, 17);
            this.chkCore_BestPairOrder.TabIndex = 10;
            this.chkCore_BestPairOrder.Text = "Best pair order";
            this.chkCore_BestPairOrder.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.chkFinal);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(233, 260);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Results";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // chkFinal
            // 
            this.chkFinal.AccessibleName = "chkFinal";
            this.chkFinal.AutoSize = true;
            this.chkFinal.Location = new System.Drawing.Point(6, 6);
            this.chkFinal.Name = "chkFinal";
            this.chkFinal.Size = new System.Drawing.Size(61, 17);
            this.chkFinal.TabIndex = 13;
            this.chkFinal.Text = "Results";
            this.chkFinal.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.chkConsole);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(233, 260);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Admin";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // chkConsole
            // 
            this.chkConsole.AccessibleName = "chkConsole";
            this.chkConsole.AutoSize = true;
            this.chkConsole.Location = new System.Drawing.Point(6, 6);
            this.chkConsole.Name = "chkConsole";
            this.chkConsole.Size = new System.Drawing.Size(152, 17);
            this.chkConsole.TabIndex = 14;
            this.chkConsole.Text = "Console (code debug only)";
            this.chkConsole.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(13, 304);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmLogging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 323);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.MaximumSize = new System.Drawing.Size(272, 361);
            this.MinimumSize = new System.Drawing.Size(272, 361);
            this.Name = "frmLogging";
            this.Text = "frmLogging";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox chkLogMessages1;
        private System.Windows.Forms.CheckBox chkLogMessages2;
        private System.Windows.Forms.CheckBox chkLogMessages3;
        private System.Windows.Forms.CheckBox chkPrepare;
        private System.Windows.Forms.CheckBox chkPrep_parameterValues;
        private System.Windows.Forms.CheckBox chkPrep_InternalParameterValues;
        private System.Windows.Forms.CheckBox chkPrep_ParameterPositions;
        private System.Windows.Forms.CheckBox chkPrep_UnusedCounts;
        private System.Windows.Forms.CheckBox chkPrep_DisplayAllPairs;
        private System.Windows.Forms.CheckBox chkPrep_DisplayAllPairs2;
        private System.Windows.Forms.CheckBox chkCore_CandidateNumber;
        private System.Windows.Forms.CheckBox chkCore_UpdateSteps;
        private System.Windows.Forms.CheckBox chkCore_AddCandidate;
        private System.Windows.Forms.CheckBox chkCore_AllCandidateTestsets;
        private System.Windows.Forms.CheckBox chkCore_ExaminePairs;
        private System.Windows.Forms.CheckBox chkCore_BestValue;
        private System.Windows.Forms.CheckBox chkCore_PlacedParams;
        private System.Windows.Forms.CheckBox chkCore_PossibleValues;
        private System.Windows.Forms.CheckBox chkCore_BestPair;
        private System.Windows.Forms.CheckBox chkCore_BestPairPosition;
        private System.Windows.Forms.CheckBox chkCore_BestPairOrder;
        private System.Windows.Forms.CheckBox chkFinal;
        private System.Windows.Forms.CheckBox chkConsole;
        private System.Windows.Forms.Button btnOK;
    }
}