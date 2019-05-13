$(document).ready(function () {
    var duration;
    var startHour;
    var endHour;

    function Time(time) {
        this.H = parseInt(time.slice(0, 2));
        this.M = parseInt(time.slice(3, 5));
        return this;
    }

    initialiseElements();

    function initialiseElements() {
        initializeHours();
        printInitialHours();
    }

    function calculateEndHour() {
        endHour = startHour + duration;
        printEndHour();
        $("#EndHour").val(endHour);
    };

    function printEndHour() {
        var endHourM = endHour % 60;
        var endHourH = (endHour - endHourM) / 60;
        $("#EndHourTime").val(clockDigit(endHourH) + ":" + clockDigit(endHourM));
    }

    function printStartHour() {
        var startHourM = startHour % 60;
        var startHourH = (startHour - startHourM) / 60;
        $("#StartHourTime").val(clockDigit(startHourH) + ":" + clockDigit(startHourM));
    }

    function printInitialHours() {
        printStartHour();
        printEndHour();
    }

    $("#StartHourTime").change(function () {
        setStartHour();
        $("#StartHour").val(startHour);
        calculateEndHour();
    });
    
    function initializeHours() {
        endHour = parseInt($("#EndHour").val(), 10);
        startHour = parseInt($("#StartHour").val(), 10);
        duration = endHour - startHour; 
    };

    function setStartHour() {
        var st = $("#StartHourTime").val();
        var startTime = Time(st);
        startHour = startTime.H * 60 + startTime.M;
    };

    function clockDigit(n) {
        return n > 9 ? "" + n : "0" + n;
    }
});