define(['knockout'], function (ko) {
    return function BillOfMaterialsModel(model) {
        var self = this;

        self.BillOfMaterialsID = ko.observable(model.PurchaseOrderID);
        self.Description = ko.observable(model.Description);
        self.Cost = ko.observable(model.Cost);
        self.Quantity = ko.observable(model.Quantity);
        self.Comments = ko.observable(model.Comments);
        self.PurchaseOrderDate = ko.observable(model.PurchaseOrderDateString);
        self.SupplierRef = ko.observable(model.SupplierRef);

        self.Total = ko.computed(function () {
            var total = self.Cost() * self.Quantity();
            return total;
        }, self);
    }
});