$(document).ready(function () {
    // Product-related functions
    window.editProduct = function (productId) {
        $(`#editForm_${productId}`).show(); // Show the edit form for the selected product
    };

    window.cancelEdit = function (productId) {
        $(`#editForm_${productId}`).hide(); // Hide the edit form
    };

    window.updateProduct = function (productId) {
        var form = $(`#editForm_${productId}`);
        var data = form.serialize(); // Serialize form data to be sent

        $.ajax({
            url: '/Administration/UpdateProduct', // Make sure this URL is correct
            type: 'POST',
            data: data,
            success: function (response) {
                if (response.success) {
                    alert('Product updated successfully');
                    location.reload(); // Reload the page to see changes
                } else {
                    alert('An error occurred: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                alert('An error occurred while updating the product.');
                console.error(error);
            }
        });
    };

    window.deleteProduct = function (productId) {
        if (confirm('Are you sure you want to delete this product?')) {
            $.ajax({
                url: '/Administration/DeleteProduct', // URL to the delete action
                type: 'POST',
                data: { id: productId },
                success: function (response) {
                    if (response.success) {
                        alert('Product deleted successfully');
                        location.reload(); // Reload the page to see changes
                    } else {
                        alert('An error occurred: ' + response.message);
                    }
                },
                error: function (xhr, status, error) {
                    alert('An error occurred while deleting the product.');
                    console.error(error);
                }
            });
        }
    };

    window.editEmployer = function (employerId) {
        console.log(`Editing employer with ID: ${employerId}`); 
        $(`#editForm_${employerId}`).show(); 
    };

    window.cancelEdit = function (employerId) {
        console.log(`Cancel editing for employer with ID: ${employerId}`);
        $(`#editForm_${employerId}`).hide(); 
    };

    window.updateEmployer = function (employerId) {
        console.log(`Updating employer with ID: ${employerId}`); 
        var form = $(`#editForm_${employerId}`);
        var isAdmin = $(`#roleCheckbox_${employerId}`).is(":checked"); 
        $(`#isAdmin_${employerId}`).val(isAdmin); 

        var data = form.serialize();

        $.ajax({
            url: '@Url.Action("UpdateEmployer", "Administration")',
            type: 'POST',
            data: data,
            success: function (response) {
                if (response.success) {
                    alert('Employer updated successfully');
                    location.reload(); 
                } else {
                    alert('An error occurred: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                alert('An error occurred while updating the employer.');
                console.error(error);
            }
        });
    };
});
