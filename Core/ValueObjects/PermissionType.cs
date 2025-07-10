namespace Core.ValueObjects;

public enum PermissionType
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