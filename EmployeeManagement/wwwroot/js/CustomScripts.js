function confirmDelete(uqiqueid, isDeleteClick)
{
    var confirmDelete = "confirmdelete_" + uqiqueid;
    var Delete = "delete_" + uqiqueid;


    if (isDeleteClick) {
        $("#" + confirmDelete).show();
        $("#" + Delete).hide();

    }
    else {
        $("#" + confirmDelete).hide();
        $("#" + Delete).show();
    }


}