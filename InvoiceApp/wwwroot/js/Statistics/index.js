//@model ViewModels.Statistics.IndexViewModel

const OVERALL = 'Overall';

drawChart('#chart');

function drawChart(selector) {
    const statistics = Model.statistics;

    if (!statistics || !statistics.length) return;

    const ctx = document.querySelector(selector);
    let chart;
    const labels = statistics[0].months.map(i => i.month)

    if (statistics.length == 1) {
        data = statistics[0];
        chart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: `${data.company?.name ?? OVERALL}(Total: ${data.amount.toLocaleString('en-US')})`,
                        data: data.months.map(i => i.amount),
                        borderWidth: 1
                    }
                ]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    }
    else {
        const colors = new Set();
        datasets = statistics.map(i => {
            let color = randomHsl();

            while (colors.has(color))
                color = randomHsl();

            return {
                label: `${i.company.name}(Total: ${i.amount.toLocaleString('en-US')})`,
                backgroundColor: color,
                borderColor: color,
                cubicInterpolationMode: 'monotone',
                data: i.months.map(j => j.amount)
            }
        });

        chart == new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: datasets
            },
            options: {
                plugins: {
                    legend: {
                        display: true,
                        position: 'right'
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                let label = context.dataset.label || '';
                                if (label) {
                                    label = label.split('(')[0];
                                }

                                if (context.parsed.y) {
                                    label += `: ${context.parsed.y.toLocaleString('en-US')}`;
                                }

                                return label;
                            }
                        }
                    },
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            }
        });
    }
}


function randomHsl() {
    return 'hsla(' + (Math.random() * 360) + ', 100%, 50%, 1)';
}


//----------------------------------------------------------------------------///


const $companySelect = document.querySelector('#company-select');
const $statisticsForm = document.querySelector('#statistics-form');
const $selectedCompanies = document.querySelector('#selected-companies');

showSelectedCompanies($selectedCompanies, $companySelect);

$companySelect.addEventListener('change', () => {
    showSelectedCompanies(
        $selectedCompanies,
        $companySelect
    );
});


function showSelectedCompanies($container, $select) {
    const companies = getSelectedCompanies($select);
    const $children = [];

    Array.from($container.children).forEach(i => {
        const isSelected = companies.includes(i.dataset.value);
        if (isSelected)
            $children.push(i);
        else
            i.remove();
    });

    if (companies.length == 0)
        companies.push(OVERALL);

    companies.forEach(i => {
        if ($children.some(j => j.dataset.value == i))
            return;

        const $span = document.createElement('span');
        $span.setAttribute('class', 'alert alert-info m-0 px-2 py-1');
        $span.setAttribute('data-value', i);
        $span.textContent = i;
        $span.addEventListener('click', () => removeSelectedCompany(i, $select))
        $container.append($span);
    });
}


function getSelectedCompanies($select) {
    const formData = new FormData($select.form);
    const selectedCompanies = formData.getAll($select.name);
    return selectedCompanies;
}


function removeSelectedCompany(company, $select) {
    for (let i = 0; i < $select.children.length; i++) {
        if ($select.children[i].value == company) {
            $select.children[i].selected = false;
            break;
        }
    }

    showSelectedCompanies($selectedCompanies, $select);
};