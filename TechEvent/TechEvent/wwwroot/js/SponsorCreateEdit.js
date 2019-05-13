$(document).ready(function () {
    populateLists();

    function populateLists() {
        var editionId = $("#EditionId").val();

        var sponsorTypeOptions = $("#SponsorTypeId").children("option");
        sponsorTypeOptions.filter((".sponsorTypeOption" + editionId)).show();
        sponsorTypeOptions.not((".sponsorTypeOption" + editionId)).hide();
        sponsorTypeOptions.removeAttr('selected');
        sponsorTypeOptions.filter((".sponsorTypeOption" + editionId)).first().attr('selected', 'selected');
    };

    $("#EditionId").change(function () { populateLists(); });
});