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

        self.ContactId = ko.observable(model.ContactID);
        self.Forename = ko.observable(model.Forename).extend({ required: true });
        self.Surname = ko.observable(model.Surname).extend({ required: true });
        self.Position = ko.observable(model.Position);
        self.Phone = ko.observable(model.Phone);
        self.Email = ko.observable(model.Email);
        self.Status = ko.observable(model.Status);

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

        var editContactSubscription = ko.postbox.subscribe("EditContact", function (callback) {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            new ajaxHelper.Post(data, '/Contact/_Edit').done(function (response, textStatus, jqXHR) {
                notifier.Notify('Saved');
                if (callback) {
                    callback(response);
                }
            }).always(function () {
                blocker.Hide();
            });
        });

        disposables.addDisposable("EditContact", editContactSubscription);

    }
});