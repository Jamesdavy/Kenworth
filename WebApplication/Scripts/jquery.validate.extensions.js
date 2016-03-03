$(document).ready(function () {
	jQuery.validator.setDefaults({
		highlight: function (element, errorClass, validClass) {
			if (element.type === 'radio') {
				this.findByName(element.name).addClass(errorClass).removeClass(validClass);
			} else {
				//$(element).addClass(errorClass).removeClass(validClass);
				$(element).closest('.form-group').removeClass('has-success').addClass('has-error');
				$(element).closest('.form-group').find('.glyphicon').attr('class', 'glyphicon glyphicon-remove');
			}
		},
		unhighlight: function (element, errorClass, validClass) {
			if (element.type === 'radio') {
				this.findByName(element.name).removeClass(errorClass).addClass(validClass);
			} else {
				//$(element).removeClass(errorClass).addClass(validClass);
				$(element).closest('.form-group').removeClass('has-error').addClass('has-success');
				$(element).closest('.form-group').find('.glyphicon').attr('class', 'glyphicon glyphicon-ok');
			}
		}
	});
});

$(function() {
    // any validation summary items should be encapsulated by a class alert and alert-danger
    $('.validation-summary-errors').each(function() {
        $(this).addClass('alert');
        $(this).addClass('alert-danger');
    });

    // update validation fields on submission of form
    $('form').submit(function() {
        $('.validation-summary-errors').each(function() {
            if ($(this).hasClass('alert-danger') == false) {
                $(this).addClass('alert');
                $(this).addClass('alert-danger');
            }
        });
    });
});

