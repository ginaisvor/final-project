$(document).ready(function () {
    populateLists();

    function populateLists() {
        var editionId = $("#EditionId").val();
        var speakerOptions = $("#SpeakerId").children("option");
        speakerOptions.filter((".speakerOption" + editionId)).show();
        speakerOptions.not((".speakerOption" + editionId)).hide();
        speakerOptions.removeAttr('selected');
        speakerOptions.filter((".speakerOption" + editionId)).first().attr('selected', 'selected');   

        var talkTypeOptions = $("#TalkTypeId").children("option");
        talkTypeOptions.filter((".talkTypeOption" + editionId)).show();
        talkTypeOptions.not((".talkTypeOption" + editionId)).hide();
        talkTypeOptions.removeAttr('selected');
        talkTypeOptions.filter((".talkTypeOption" + editionId)).first().attr('selected', 'selected');  
    };

    $("#EditionId").change(function () { populateLists(); });
});