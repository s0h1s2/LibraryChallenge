namespace Core.ValueObjects;

public enum AttributeType
{
    CanViewBooks,
    CanCreateBooks,
    CanUpdateBooks,
    CanDeleteBooks,

    CanViewUsers,
    CanCreateUsers,
    CanUpdateUsers,
    CanDeleteUsers,

    CanBorrowBooks,
    CanReturnBooks,
    CanViewBorrowingHistory,
    CanExtendDueDate,

    CanViewReports,
    CanViewAllBorrowingHistory,
    CanManagePermissions,
}