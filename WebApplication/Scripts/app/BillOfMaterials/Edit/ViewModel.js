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

        self.PurchaseOrderID = ko.observable(model.PurchaseOrderID);
        self.Description = ko.observable(model.Description).extend({ required: true });
        self.Cost = ko.observable(model.Cost).extend({ required: true });
        self.Quantity = ko.observable(model.Quantity).extend({ required: true });
        self.Comments = ko.observable(model.Comments);
        self.PurchaseOrderDate = ko.observable(model.PurchaseOrderDateString).extend({ required: true });
        self.SupplierRef = ko.observable(model.SupplierRef);

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

        var addBOMSubscription = ko.postbox.subscribe("EditBOM", function (callback) {
            blocker.Show();
            var data = ajaxHelper.ToServerJson(self);
            new ajaxHelper.Post(data, '/BillOfMaterials/_Edit').done(function (response, textStatus, jqXHR) {
                notifier.Notify('Saved');
                if (callback) {
                    callback(response);
                }
            }).always(function () {
                blocker.Hide();
            });
        });

        disposables.addDisposable("EditBOM", addBOMSubscription);

    }
});