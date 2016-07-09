' Simulates the actual class used in the API
Public Class DataServiceKey
	Inherits Attribute

	Private _dataServiceKey As String


	Public Sub New(dataServiceKey As String)
		_dataServiceKey = dataServiceKey
	End Sub

	Public Sub New(dataServiceKey As String, dataServiceKey2 As String)
		_dataServiceKey = dataServiceKey
	End Sub

	Public Sub New(dataServiceKey As String, dataServiceKey2 As String, dataServiceKey3 As String)
		_dataServiceKey = dataServiceKey
	End Sub

	Public Property DataServiceKey() As String
		Get
			Return _dataServiceKey
		End Get
		Set(Value As String)
			_dataServiceKey = Value
		End Set
	End Property
End Class