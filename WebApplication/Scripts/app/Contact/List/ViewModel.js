define(['knockout',
    'app/Contact/List/ContactModel',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'app/Common/UIBlocker',
    'app/Common/Notification',
    'knockout.validation',
    'knockout-postbox',
    'jquery.autocomplete.min'], function (ko, contactModel, ajaxHelper, disposables, uiBlocker, notification) {
    return function ViewModel(model) {
        var self = this;

        //var blocker = new uiBlocker();
        //var notifier = new notification();

        self._Contacts = ko.observableArray([]);
        var mappedContacts = $.map(model.Contacts, function (item) { return new contactModel(item); });
        self._Contacts(mappedContacts);

        

        //ko.validation.init({
        //    registerExtenders: true,
        //    messagesOnModified: false,
        //    insertMessages: false,
        //    parseInputAttributes: true,
        //    messageTemplate: null,
        //    decorateInputElement: true,
        //    errorElementClass: 'has-error',
        //    errorAsTitle: true,
        //    grouping: {
        //        deep: true
        //    }
        //}, true);

        //var editContactSubscription = ko.postbox.subscribe("EditContact", function (callback) {
        //    blocker.Show();
        //    var data = ajaxHelper.ToServerJson(self);
        //    alert(data);
        //    new ajaxHelper.Post(data, '/Contact/_Edit').done(function (response, textStatus, jqXHR) {
        //        notifier.Notify('Saved');
        //        if (callback) {
        //            callback(response);
        //        }
        //    }).always(function () {
        //        blocker.Hide();
        //    });
        //});

        //disposables.addDisposable("EditContact", editContactSubscription);

    }
});