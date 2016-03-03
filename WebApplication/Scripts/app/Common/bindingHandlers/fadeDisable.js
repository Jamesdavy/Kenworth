define(['knockout'],
    function (ko) {

        // Here's a custom Knockout binding that makes elements shown/ 
        //hidden via jQuery's fadeIn()/fadeOut() methods 
        // Could be stored in a separate utility library
        ko.bindingHandlers.fadeDisable = {
            init: function (element, valueAccessor) {
                // Initially set the element to be instantly visible/ 
                // hidden depending on the value  
                //var value = valueAccessor();
                //$(element).toggle(ko.utils.unwrapObservable(value))
                //$(element).toggle(ko.utils.unwrapObservable(value)); // 
                //Use "unwrapObservable" so we can handle values that may or may not be observable 
            },
            update: function (element, valueAccessor) {
                // Whenever the value subsequently changes, slowly fade the element in or out 
                var value = valueAccessor();
                if (ko.utils.unwrapObservable(value)) {
                    $(element).fadeTo('slow', 1);
                    $(element).find('input, textarea, button, select').not(".ignore").removeAttr('disabled');
                    $(element).find('a').each(function (e) {
                        $(this).removeClass('check-item-link-inactive');
                    });
                } else {
                    $(element).fadeTo('slow', 0.25);
                    $(element).find('input, textarea, button, select').not(".ignore").attr('disabled', true);
                    $(element).find('a').each(function (e) {
                        $(this).addClass('check-item-link-inactive');
                    });

                    //$(element).find('a').addClass('check-item-link-inactive').removeClass('check-item-link-active');
                }
            }
        };
    });