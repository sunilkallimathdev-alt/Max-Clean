$(document).ready(function(){
	'use strict';

	Highcharts.chart('chrt1', {
		colors: ['#dadada','#67ba5f','#8bbc21','#1aadce'],
		chart: {
			type: 'area',
			backgroundColor: "#FFFFFF",
			borderColor: "#335cad"
		},
		title: {
			text: 'US and USSR nuclear stockpiles'
		},
		xAxis: {
			categories: ['January', 'February', 'March', 'April', 'May', 'June', 'July']
		},
		credits: {
			enabled: false
		},
		tooltip: {
			pointFormat: '{series.name} produced <b>{point.y:,.0f}</b><br/>warheads in {point.x}'
		},   	
		plotOptions: {
			area: {
				categories: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
				marker: {
					enabled: false,
					symbol: 'circle',
					radius: 2,
					states: {
						hover: {
							enabled: true
						}
					}
				}
			}
		},
		series: [{
			data: [18,45,35,10,85,25,90]
		}, {
			data: [45,50,25,85,55,65,70]
		}]
	});
});