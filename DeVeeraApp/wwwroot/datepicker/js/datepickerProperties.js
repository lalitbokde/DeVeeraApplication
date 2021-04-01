(function($){
    $(function(){
        $('#id_0').datetimepicker({
            "allowInputToggle": true,
            "showClose": true,
            "showClear": true,
            "showTodayButton": true,
            "format": "MM/DD/YYYY hh:mm:ss A",
            icons: {
                time: "fa fa-clock-o",
                today: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down",
                close: "fa fa-arrow-down"
            }
        });
        $('#id_1').datetimepicker({
            "allowInputToggle": true,
            "showClose": true,
            "showClear": true,
            "showTodayButton": true,
            "format": "MM/DD/YYYY HH:mm:ss",
        });
        $('#WorkRequestTime').datetimepicker({
            "allowInputToggle": true,
            "showClose": true,
            "showClear": true,
            "showTodayButton": true,
            "format": "hh:mm:ss A",
        });
        $('#id_3').datetimepicker({
            "allowInputToggle": true,
            "showClose": true,
            "showClear": true,
            "showTodayButton": true,
            "format": "HH:mm:ss",
        });
        

        $('.date').datetimepicker({
            "allowInputToggle": true,
            "showClose": false,
            "showClear": true,
            "showTodayButton": true,
            "format": "MM/DD/YYYY",
            "icons": {
                time: "fa fa-clock-o",
                "today": "i-Calendar",
                "clear": "i-Close-Window",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down",
                close: "fa fa-arrow-down"
            }
        });

  
      

    });
})(jQuery);
