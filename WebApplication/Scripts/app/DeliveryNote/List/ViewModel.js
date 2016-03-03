define(['knockout',
    'app/DeliveryNote/List/DeliveryNoteModel',
    'app/Common/AjaxHelper',
    'app/Common/Disposables',
    'app/Common/UIBlocker',
    'app/Common/Notification',
    'knockout.validation',
    'knockout-postbox',
    'jquery.autocomplete.min'], function (ko, deliveryNoteModel, ajaxHelper, disposables, uiBlocker, notification) {
    return function ViewModel(model) {
        var self = this;

        var blocker = new uiBlocker();
        var notifier = new notification();

        self.JobId = model.JobId;
        self._DeliveryNotes = ko.observableArray([]);
        var mappedDeliveryNotes = $.map(model.DeliveryNotes, function (item) { return new deliveryNoteModel(item); });
        self._DeliveryNotes(mappedDeliveryNotes);

        self.SelectedDeliveryNotes = ko.observableArray();

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

        var createDeliveryNoteSubscription = ko.postbox.subscribe("CreateDeliveyNote", function (callback) {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            new ajaxHelper.Post(data, '/DeliveryNote/_Create').done(function (response, textStatus, jqXHR) {
                notifier.Notify('Saved');
                if (callback) {
                    callback(response);
                }
            }).always(function () {
                blocker.Hide();
            });
        });

        disposables.addDisposable("CreateDeliveyNote", createDeliveryNoteSubscription);

    }
});