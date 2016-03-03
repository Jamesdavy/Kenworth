
define(['knockout'], function(ko) {
    var windowURL = window.URL || window.webkitURL;

    ko.bindingHandlers.file = {
        init: function(element, valueAccessor) {
            $(element).change(function() {
                var file = this.files[0];
                if (ko.isObservable(valueAccessor())) {
                    valueAccessor()(file);
                }
            });
        },

        update: function(element, valueAccessor, allBindingsAccessor) {
            var file = ko.utils.unwrapObservable(valueAccessor());
            var bindings = allBindingsAccessor();

            if (bindings.fileObjectURL && ko.isObservable(bindings.fileObjectURL)) {
                var oldUrl = bindings.fileObjectURL();
                if (oldUrl) {
                    windowURL.revokeObjectURL(oldUrl);
                }
                bindings.fileObjectURL(file && windowURL.createObjectURL(file));
            }

            if (bindings.fileBinaryData && ko.isObservable(bindings.fileBinaryData)) {
                if (!file) {
                    bindings.fileBinaryData(null);
                    bindings.fileType(null);
                } else {
                    bindings.fileType(file.type);
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var result = e.target.result || {};
                        var resultParts = result.split(",");
                        if (resultParts.length === 2) {
                            bindings.fileBinaryData(resultParts[1]);
                            //bindings.fileType(resultParts[0]);
                        }
                    };
                    //reader.readAsArrayBuffer(file);
                    reader.readAsDataURL(file);
                }
            }
        }
    };

    
    
});
