define(['jquery', 'jquery.blockUI'], function (common) {
    return function UIBlocker() {
        var self = this;

        self.Show = function () {
            $.blockUI({
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                },
                baseZ: 10000,
                message: common.saving
            });
        };

        self.Hide = function () {
            $.unblockUI();
        };
    }
});