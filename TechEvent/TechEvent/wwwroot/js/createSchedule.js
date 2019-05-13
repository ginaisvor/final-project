$(document).ready(function () {
    var duration;
   // var breakTime;
    var startHour;
    var endHour;

    function Time(time) {
        this.H = parseInt(time.slice(0, 2));
        this.M = parseInt(time.slice(3, 5));
        return this;
    }

    initialiseElements();

    function initialiseElements() {
        setDuration();
       // setBreakTime();
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
        startHour = parseInt($("#StartHour").val(), 10);
        printStartHour();
        endHour = startHour + duration;
        printEndHour();

    }

    $("#StartHourTime").change(function () {
        setStartHour();
        $("#StartHour").val(startHour);
        calculateEndHour();
    });

    $("#BreakAfter").change(function () {
        setBreakTime();
    });

    $("#TalkId").change(function () {
        setDuration();
        calculateEndHour();
    });

    function setDuration() {
        duration = parseInt($("#TalkId option:selected").attr("duration"), 10);
    };
    /*
    function setBreakTime() {
        var breakTimeRead = parseInt($("#BreakAfter").val(), 10);
        if (breakTimeRead >= 0 && breakTimeRead <= 120) {
            breakTime = breakTimeRead;
        } else {
            breakTime = 0;
        }
    }
    */
    function setStartHour() {
        var st = $("#StartHourTime").val();
        var startTime = Time(st);
        startHour = startTime.H * 60 + startTime.M;
    };

    function clockDigit(n) {
        return n > 9 ? "" + n : "0" + n;
    }
});