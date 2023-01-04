//http://codepen.io/dprantzalos/pen/ojabgO
$(document).ready(function () {
    /**
     * Add onclick handler to button to toggle table style from 'table-responsive' to 'jim-table-responsive'.
     */
    $('button#toggle').click(function () {
        // toggle button color/text and change table style class
        if ($(this).is('.btn-primary')) {
            $(this).removeClass('btn-primary').addClass('btn-success').html("Click to use Bootstrap Table");
            $('div#data').removeClass('table-responsive').addClass('jim-table-responsive');
        } else {
            $(this).removeClass('btn-success').addClass('btn-primary').html("Click to use My Table");
            $('div#data').removeClass('jim-table-responsive').addClass('table-responsive');
        }
    });
});
