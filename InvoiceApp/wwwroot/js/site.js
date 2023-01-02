initSearchForm();


function initSearchForm() {
    document.querySelectorAll('.search-form').forEach($form => $form.addEventListener('submit', (e) => {
        e.preventDefault();
        const formData = new FormData($form);

        [...formData.keys()].forEach(key => formData.get(key) || formData.delete(key));

        const url = new URL($form.action)
        for (const [key, value] of formData.entries()) {
            url.searchParams.append(key, value);
        }

        window.location.href = url.href;
    }));
}