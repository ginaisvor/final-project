$(document).ready(function () {

    function changeEdition() {
        var yearId = $("#CurrentViewEdition").val();
       /* $.post("Home/ChangeEdition", { "editionId": yearId });
        var BaseUrl = 'https://' + top.location.host;*/

        $.ajax({
            type: 'POST',
            url: BaseUrl + '/Home/ChangeEdition',
            data: ({ "editionId": yearId }),
            success: function (resp) {

            }
        });

    };

    $("#CurrentViewEdition").change(function () { changeEdition(); });
});