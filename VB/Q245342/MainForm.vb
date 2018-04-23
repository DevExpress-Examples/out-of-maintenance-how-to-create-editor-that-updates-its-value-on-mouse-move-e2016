Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Namespace Q245342
	Partial Public Class MainForm
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			dataTable1.Rows.Add(0D)
		End Sub
	End Class
End Namespace
