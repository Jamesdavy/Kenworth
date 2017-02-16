define(['knockout'], function (ko) {
    return function DeliveryNoteModel(model) {
        var self = this;

        self.LineId = ko.observable(model.LineID);
        self.UniqueId = ko.observable(model.UniqueId);
        self.Description = ko.observable(model.Description);
        self.ExpectedDeliveryDate = ko.observable(model.ExpectedDeliveryDate);
        self.Quantity = ko.observable(model.Quantity);
        self.QuantityAlreadyDispatched = ko.observable(model.QuantityAlreadyDispatched);
        self.QuantityToDispatch = ko.observable(0);
        self.LastQuantityDispatched = ko.observable(model.LastQuantityDispatched);

        self.QuantityLeftToDispatch = function () {
            return parseFloat(self.Quantity()) - parseFloat(self.QuantityAlreadyDispatched()) - parseFloat(self.QuantityToDispatch());
        };

        self.HasItemsToDispatch = function () {
            return (parseFloat(self.QuantityToDispatch()) > parseFloat(0) && self.QuantityLeftToDispatch() >= 0) && parseFloat(self.QuantityAlreadyDispatched()) >= 0;
        };

        self.HasAllItemsDispatched = function () {
            var a = parseFloat(self.QuantityAlreadyDispatched());
            var b = parseFloat(self.Quantity());
            return b > a;
        };

        self.HasTooManyDispatchItems = function () {
            return self.QuantityLeftToDispatch() < 0 || parseFloat(self.QuantityAlreadyDispatched()) < 0;
        };

        self.HasBeenPreviouslyDispatched = function () {
            return self.DeliveryNoteSent() === true;
        };

        self.DeliveryNoteSent = ko.observable(model.DeliveryNoteSent);
        self.SentStatus = ko.observable(model.SentStatus);
        //self.SendDeliveryNote = ko.observable();
    }
});