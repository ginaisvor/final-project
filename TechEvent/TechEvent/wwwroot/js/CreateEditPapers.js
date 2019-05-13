$(document).ready(function () {
    populateLists();

    function populateLists() {
        var editionId = $("#EditionId").val();

        var talkTypeOptions = $("#TalkTypeId").children("option");
        talkTypeOptions.filter((".talkTypeOption" + editionId)).show();
        talkTypeOptions.not((".talkTypeOption" + editionId)).hide();
        talkTypeOptions.removeAttr('selected');
        talkTypeOptions.filter((".talkTypeOption" + editionId)).first().attr('selected', 'selected');
    };

    $("#EditionId").change(function () { populateLists(); });
});