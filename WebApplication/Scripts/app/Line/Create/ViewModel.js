define(['knockout',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'app/Common/UIBlocker',
    'app/Common/Notification',
    'knockout.validation',
    'knockout-postbox',
    'knockout-kendo',
    'jquery.autocomplete.min'
    //'Common/Validation/globalFloat'
], function (ko, ajaxHelper, disposables, uiBlocker, notification) {
    return function ViewModel(lineDetail) {
        var self = this;

        var blocker = new uiBlocker();
        var notifier = new notification();

        self.JobId = ko.observable(lineDetail.JobId);
        self.Description = ko.observable().extend({ required: true });
        self.Quantity = ko.observable().extend({ required: true });
        self.UnitPrice = ko.observable().extend({ required: true });
        self.ExpectedDeliveryDate = ko.observable(lineDetail.ExpectedDeliveryDateString).extend({ required: true });
        self.DeliveryComments = ko.observable();
        self.DrawingNumber = ko.observable();

        self.EstimatedHours = ko.observable().extend({ required: true });;
        self.EstimatedHourlyRate = ko.observable(lineDetail.EstimatedHourlyRate).extend({ required: true });;
        self.CustomerRef = ko.observable();
        //self._FileUpload = ko.observable();
        //self.Debug = ko.computed(function () {
        //    return ajaxHelper.ToServerJson(self);
        //}, self);


        self.File = ko.observable();
        self.FileObjectURL = ko.observable();
        self.FileBinary = ko.observable();
        self.FileType = ko.observable();

        self.FileSize = ko.computed(function() {
            var file = this.File();
            return file ? file.size : 0;
        }, self);

        /*Recieve External Request to Add new Item*/
        var addLineSubscription = ko.postbox.subscribe("AddLine", function (callback) {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            new ajaxHelper.Post(data, '/Line/_Create').done(function (response, textStatus, jqXHR) {
                notifier.Notify('Saved');
                callback(response);
            }).always(function () {
                blocker.Hide();
            });
        });

        disposables.addDisposable("AddLine", addLineSubscription);

        $("#LineDescriptionSearch").autocompleteold('/Json/_GetMatchingLineDescription', {
            autoFill: false,
            mustMatch: false,
            matchContains: false,
            cacheLength: 12,
            formatItem: function (data, index, max) {
                return data[1];
            },
            formatMatch: function (data, index, max) {
                return data[1];
            },
            formatResult: function (data, index, max) {
                return data[1];
            }
        }).result(function (event, data, formatted) {
            if (data) {
                self.Description(data[1]);
            } else {
                self.Description('');
                self.LineId(0);
            }
        });

    }
});