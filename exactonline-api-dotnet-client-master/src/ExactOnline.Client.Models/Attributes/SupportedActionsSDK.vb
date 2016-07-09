Public Class SupportedActionsSDK
	Inherits Attribute

	Private _canCreate As Boolean
	Private _canRead As Boolean
	Private _canUpdate As Boolean
	Private _canDelete As Boolean

	Public Sub New(canCreate As Boolean, canRead As Boolean, canUpdate As Boolean, canDelete As Boolean)
		_canCreate = canCreate
		_canRead = canRead
		_canUpdate = canUpdate
		_canDelete = canDelete
	End Sub

	Public ReadOnly Property CanCreate() As Boolean
		Get
			Return _canCreate
		End Get
	End Property

	Public ReadOnly Property CanRead() As Boolean
		Get
			Return _canRead
		End Get
	End Property

	Public ReadOnly Property CanUpdate() As Boolean
		Get
			Return _canUpdate
		End Get
	End Property

	Public ReadOnly Property CanDelete() As Boolean
		Get
			Return _canDelete
		End Get
	End Property

End Class
