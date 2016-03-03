define(['knockout'], function (ko) {
    return function DeliveryNoteModel(model) {
        var self = this;

        self.LineId = ko.observable(model.LineID);
        self.UniqueId = ko.observable(model.UniqueId);
        self.Description = ko.observable(model.Description);
        self.ExpectedDeliveryDate = ko.observable(model.ExpectedDeliveryDate);
        self.Quantity = ko.observable(model.Quantity);
        self.DeliveryNoteSent = ko.observable(model.DeliveryNoteSent);
        self.SentStatus = ko.observable(model.SentStatus);

    }
});