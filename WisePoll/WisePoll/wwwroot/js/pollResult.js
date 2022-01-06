$(document).ready(() => {
    const totalVotes = $('*[data-chart-bar]').data('totalVotes')
    $('*[data-chart-bar] .result-charts-bar').each((i,e) => {
        const votes = $(e).data('votes');
        const percentages = (parseInt(votes) / parseInt(totalVotes)) * 100
        e.style.width = `${percentages}%`
    })
})