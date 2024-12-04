Imports System.Windows.Forms
Module mdlVariables
    Public thisUser As New CurrentUser
    Public thisTransanction As Transaction
    Public Enum MessageBoxType
        _Error = MessageBoxIcon.Error
        _Info = MessageBoxIcon.Information
    End Enum
    Public Structure CurrentUser
        Public keyid
        Public FullName
        Public Username
        Public Firstname
        Public Lastname
        Public Nickname
        Public UserFunction
        Public IsAdmin As Boolean
        Public IsSuperAdmin As Boolean
        Public Access As List(Of String)
    End Structure

    Public Structure Transaction
        Public ReferenceNo
        Public TransactionDate
        Public CustomerName
        Public PlateNo
        Public DriverName
        Public Weigher
        Public Product
        Public ModeOfPayment
        Public Amount
        Public Gross
        Public Tare
        Public Net
        Public GrossCapturedDate
        Public TareCapturedDate
        Public Remarks
    End Structure

    Public Structure InsertParameters
        Public FieldName
        Public Value
    End Structure

End Module
