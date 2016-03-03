define(['knockout',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'app/Common/UIBlocker',
    'app/Common/Notification',
    'knockout.validation',
    'knockout-postbox',
    'jquery.autocomplete.min'], function (ko, ajaxHelper, disposables, uiBlocker, notification) {
    return function ViewModel(model) {
        var self = this;

        var blocker = new uiBlocker();
        var notifier = new notification();
        var initialDate = model.PurchaseOrderDateString;

        self.LineId = ko.observable(model.LineID);
        self.Description = ko.observable().extend({ required: true });
        self.JobLineDescription = ko.observable().extend({ required: true });
        self.Cost = ko.observable().extend({ required: true });
        self.Quantity = ko.observable().extend({ required: true });
        self.Comments = ko.observable();
        self.PurchaseOrderDate = ko.observable(model.PurchaseOrderDateString).extend({ required: true });
        self.SupplierRef = ko.observable();

        ko.validation.init({
            registerExtenders: true,
            messagesOnModified: false,
            insertMessages: false,
            parseInputAttributes: true,
            messageTemplate: null,
            decorateInputElement: true,
            errorElementClass: 'has-error',
            errorAsTitle: true,
            grouping: {
                deep: true
            }
        }, true);

        var addBOMSubscription = ko.postbox.subscribe("AddBOM", function (callback) {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            new ajaxHelper.Post(data, '/BillOfMaterials/_Create').done(function (response, textStatus, jqXHR) {
                notifier.Notify('Saved');
                self.Description('');
                self.Cost('');
                self.Quantity('');
                self.Comments('');
                self.PurchaseOrderDate(initialDate);
                if (callback) {
                    callback(response);
                }
            }).always(function () {
                blocker.Hide();
            });
        });

        disposables.addDisposable("AddBOM", addBOMSubscription);

        $("#Search").autocompleteold('/Json/_GetMatchingJobLineID', {
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
                self.JobLineDescription(data[1]);
                self.LineId(data[0]);
            } else {
                self.JobLineDescription('');
                self.LineId(0);
            }
        });

        $("#BOMDescriptionSearch").autocompleteold('/Json/_GetMatchingBOMDescription', {
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
            }
        });
    }
});