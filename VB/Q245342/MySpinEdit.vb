Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Registrator

Namespace DXSample
	Public Class MySpinEdit
		Inherits SpinEdit
		Shared Sub New()
			RepositoryItemMySpinEdit.RegisterMySpinEdit()
		End Sub
		Public Sub New()
			MyBase.New()
			fValueUpdateTimer = New Timer()
			fValueUpdateTimer.Interval = 200
			AddHandler ValueUpdateTimer.Tick, Function(sender, e) AnonymousMethod1(sender, e)
			ValueUpdateStep = 0D
		End Sub
		
		Private Function AnonymousMethod1(ByVal sender As Object, ByVal e As EventArgs) As Boolean
			Value += ValueUpdateStep
			Return True
		End Function



		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return RepositoryItemMySpinEdit.MySpinEditName
			End Get
		End Property

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Shadows ReadOnly Property Properties() As RepositoryItemMySpinEdit
			Get
				Return CType(MyBase.Properties, RepositoryItemMySpinEdit)
			End Get
		End Property

		Private fValueUpdateTimer As Timer
		Private ReadOnly Property ValueUpdateTimer() As Timer
			Get
				Return fValueUpdateTimer
			End Get
		End Property
		Private fValueUpdateStep As Decimal
		Private Property ValueUpdateStep() As Decimal
			Get
				Return fValueUpdateStep
			End Get
			Set(ByVal value As Decimal)
				fValueUpdateStep = value
			End Set
		End Property

		Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
			MyBase.OnMouseMove(e)
			If e.Button = MouseButtons.Left Then
				ValueUpdateStep = If(e.Location.Y > Height / 2, -1D, 1D)
				If ViewInfo.CalcHitInfo(e.Location).HitTest = EditHitTest.Button Then
					ValueUpdateTimer.Start()
				End If
			End If
		End Sub

		Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
			MyBase.OnMouseUp(e)
			If ValueUpdateTimer.Enabled Then
				ValueUpdateTimer.Stop()
			End If
		End Sub
	End Class

	<UserRepositoryItem("RegisterMySpinEdit")> _
	Public Class RepositoryItemMySpinEdit
		Inherits RepositoryItemSpinEdit
		Shared Sub New()
			RegisterMySpinEdit()
		End Sub
		Public Sub New()
			MyBase.New()
		End Sub

		Friend Const MySpinEditName As String = "MySpinEdit"

		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return MySpinEditName
			End Get
		End Property

		Public Shared Sub RegisterMySpinEdit()
			EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(MySpinEditName, GetType(MySpinEdit), GetType(RepositoryItemMySpinEdit), GetType(BaseSpinEditViewInfo), New ButtonEditPainter(), True))
		End Sub
	End Class
End Namespace