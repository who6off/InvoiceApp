// @model ViewModels.Invoice.ListViewModel
// Statuses: string[];

const $checkboxes = document.querySelectorAll('.status-checkbox');

$checkboxes.forEach($checkbox => $checkbox.addEventListener('change', e => {
    if (!$checkbox.checked) {
        const $input = $checkbox.form.querySelector(`input[value="${$checkbox.dataset.value}"]`);
        if ($input) $input.remove();
        return;
    }

    $checkbox.form.append(createHiddenInput($checkbox.dataset.value));
}));

initCheckboxes($checkboxes, Statuses);


function createHiddenInput(value) {
    const $input = document.createElement('input');
    $input.setAttribute('type', 'hidden');
    $input.setAttribute('name', 'Status');
    $input.setAttribute('value', value);
    return $input;
}


function initCheckboxes($checkboxes, statuses) {
    if (!statuses || statuses.length < 1) return;

    $checkboxes.forEach($checkbox => {
        if (statuses.includes($checkbox.dataset.value)) {
            $checkbox.checked = true;
            $checkbox.dispatchEvent(new Event('change'));
        }
    })
}