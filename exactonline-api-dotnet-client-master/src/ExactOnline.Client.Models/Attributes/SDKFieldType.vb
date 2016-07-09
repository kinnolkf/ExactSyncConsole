' Attribute specifies the type of field (for read only fields)
Imports System

Public Class SDKFieldType
	Inherits Attribute

	Public Property TypeOfField() As FieldType

	Public Sub New(fieldType As FieldType)
		TypeOfField = fieldType
	End Sub

End Class
