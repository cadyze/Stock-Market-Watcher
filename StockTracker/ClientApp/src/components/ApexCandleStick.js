import React from "react";
import Chart from "react-apexcharts";
import "./StockData.css"

function ApexCandleStick(props) {
    let options =
    {
        chart: {
            type: "candlestick",
            fontFamily: "alte-haas-grotesk",
            height: 350,
        },
        title: {
            text: "Stock Chart for " + props.name,
            align: "center",
        },
        xaxis: {
            type: "datetime",
        },
        yaxis: {
            tooltip: {
                enabled: true,
            },
        },
        plotOptions: {

            candlestick: {
                colors: {
                    upward: '#1E88E5',
                    downward: "#FF8A65"
                },
                wick: {
                    useFillColor: true
                }
            }
        }
    }
    return (
        <div>
            <Chart
                type="candlestick"
                height={400}
                options={options}
                series={props.series}
            />
        </div>
    );
}
export default ApexCandleStick;