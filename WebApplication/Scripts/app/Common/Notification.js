define(['jquery', 'bootstrap-notify'],
    function () {
        return function Notification() {
            var self = this;
            self.Notify = function (msg) {
                return $.notify({
                    icon: 'glyphicon glyphicon-ok',
                    title: 'Success',
                    message: msg
                }, {
                    delay: 1000,
                    'type': 'success',
                    z_index: 10000,
                    placement: {
                        from: "bottom",
                        align: "left"
                    },
                    animate: {
                        enter: 'animated fadeInUp',
                        exit: 'animated fadeOutDown'
                    },
                    template: '<div data-notify="container" class="col-xs-11 col-sm-5 col-lg-2 col-md-5 alert-{0}" role="alert">' +
                        '<div class="row header">' +
                        '<span data-notify="icon" class="pull-left"></span>' +
                        '<span data-notify="title">{1}</span>' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button>' +
                        '</div>' +
                        '<div class="body row clear_both hidden-xs hidden-sm">' +
                        '<span data-notify="message">{2}</span>' +
                        '</div>' +
                        '</div>'
                });
            };

            self.Error = function (msg) {
                return $.notify({
                    icon: 'glyphicon glyphicon-exclamation-sign',
                    title: common.error,
                    message: msg
                }, {
                    element: 'body',
                    'type': 'error',
                    delay: 0,
                    z_index: 1030,

                    placement: {
                        from: "top",
                        align: "center"
                    },
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    template: '<div data-notify="container" class="col-xs-11 col-sm-5 alert-{0}" role="alert">' +
                        '<div class="row header">' +
                        '<span data-notify="icon" class="pull-left"></span>' +
                        '<span data-notify="title">{1}</span>' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button>' +
                        '</div>' +
                        '<div class="body row clear_both">' +
                        '<span data-notify="message">{2}</span>' +
                        '</div>' +
                        '</div>'
                });
            };

            self.Retry = function (msg) {
                return $.notify({
                    icon: 'glyphicon glyphicon-repeat',
                    title: common.retry,
                    message: notifications.retryMessage
                }, {
                    delay: 0,
                    'type': 'retry',
                    z_index: 1030,
                    placement: {
                        from: "top",
                        align: "center"
                    },
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    template: '<div data-notify="container" class="col-xs-11 col-sm-5 alert-{0}" role="alert">' +
                        '<div class="row header">' +
                        '<span data-notify="icon" class="pull-left"></span>' +
                        '<span data-notify="title">{1}</span>' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button>' +
                        '</div>' +
                        '<div class="body row clear_both">' +
                        '<span data-notify="message">{2}</span>' +
                        '</div>' +
                        '</div>'
                });
            };

            self.ErrorFlash = function (msg) {
                return $.notify({
                    icon: 'glyphicon glyphicon-exclamation-sign',
                    title: 'Error',
                    message: msg
                }, {
                    element: 'body',
                    'type': 'error',
                    delay: 2000,
                    z_index: 10000,

                    placement: {
                        from: "top",
                        align: "center"
                    },
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    template: '<div data-notify="container" class="col-xs-11 col-sm-5 alert-{0}" role="alert">' +
                        '<div class="row header">' +
                        '<span data-notify="icon" class="pull-left"></span>' +
                        '<span data-notify="title">{1}</span>' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button>' +
                        '</div>' +
                        '<div class="body row clear_both">' +
                        '<span data-notify="message">{2}</span>' +
                        '</div>' +
                        '</div>'
                });
            };

            self.closeAll = function () {
                $.notifyClose();
            }
        }
    });