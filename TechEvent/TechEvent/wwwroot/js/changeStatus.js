$(document).ready(function () {
    var selectList = $("#PaperStatusId");
    var currentStatus = selectList.val();
    var n = selectList.children().length;

    if (currentStatus == 1) {
        selectList.children().filter(function () {
            $(this).toggle($(this).val() < 3)
        });
    } else if (currentStatus == 2) {
            selectList.children().filter(function () {
                $(this).toggle($(this).val() > 1)
            });
        }
     else {
        selectList.children().filter(function () {
            $(this).toggle($(this).val() == currentStatus)
        });
    }
});