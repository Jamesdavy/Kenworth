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
        
        self.ClientId = ko.observable(model.ClientId);
        self.Client = ko.observable();
        self.Forename = ko.observable().extend({ required: true });
        self.Surname = ko.observable().extend({ required: true });
        self.Position = ko.observable();
        self.Phone = ko.observable();
        self.Email = ko.observable();

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

        var addContactSubscription = ko.postbox.subscribe("AddContact", function (callback) {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            new ajaxHelper.Post(data, '/Contact/_Create').done(function (response, textStatus, jqXHR) {
                notifier.Notify('Saved');
                //self.Description('');
                //self.Cost('');
                //self.Quantity('');
                //self.Comments('');
                //self.PurchaseOrderDate(initialDate);
                if (callback) {
                    callback(response);
                }
            }).always(function () {
                blocker.Hide();
            });
        });

        disposables.addDisposable("AddContact", addContactSubscription);

        $("#Search").autocompleteold('/Json/_GetMatchingClients', {
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
                self.ClientId(data[0]);
                self.Client(data[1]);
            } else {
                self.ClientId('');
                self.Client('');
            }
        });
    }
});