$(document).ready(function(){
	'use strict';

	Highcharts.chart('anyl-chrt', {
		colors: ['#67ba5f','#ffd631','#8bbc21','#1aadce'],
		chart: {
			type: 'column'
		},
		title: {
			text: 'Stacked column chart'
		},
		xAxis: {
			categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug']
		},
		yAxis: {
			min: 0,
			title: {
				text: 'Total fruit consumption'
			}
		},
		tooltip: {
			pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.percentage:.0f}%)<br/>',
			shared: true
		},
		plotOptions: {
			column: {
				stacking: 'percent'
			}
		},
		series: [{
			name: '',
			data: [5, 3, 4, 7, 2, 6, 4, 5]
		}, {
			name: '',
			data: [2, 2, 3, 2, 1, 5, 6, 4]
		}]
	});
});