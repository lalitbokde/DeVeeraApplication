



$(function () {
    var smsCodes = $('.smsCode');
    function goToNextInput(e) {
        var key = e.which,
            t = $(e.target),
            // Get the next input
            sib = t.closest('div').next().find('.smsCode');

        // Not allow any keys to work except for tab and number
        if (key != 9 && (key < 48 || key > 57)) {
            console.log("!=9");
            e.preventDefault();
            return false;
        }

        // Tab
        if (key === 9) {
            console.log("===9");
            return true;
        }

        // Go back to the first one
        if (!sib || !sib.length) {
            console.log("!sib || !sib.length");

            sib = $('.smsCode').eq(0);
            console.log(sib);
        }
        sib.select().focus();
    }

    function onKeyDown(e) {
        var key = e.which;

        // only allow tab and number
        if (key === 9 || (key >= 48 && key <= 57)) {
            return true;
        }

        e.preventDefault();
        return false;
    }

    function onFocus(e) {
        $(e.target).select();
    }

    smsCodes.on('keyup', goToNextInput);
    smsCodes.on('keydown', onKeyDown);
    smsCodes.on('click', onFocus);

})
