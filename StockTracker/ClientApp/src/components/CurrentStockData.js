import React, { useState, useEffect, useRef } from 'react';
import { Layout } from './Layout';
import ApexCandleStick from "./ApexCandleStick";
import styles from "./StockData.css"

export function CurrentStockData() {
    const [loading, setLoading] = useState(true);
    const [ticker, setTicker] = useState('');
    const [stock, setStock] = useState("");
    //const [isInvalidStock, setIsInvalidStock] = useState(true);
    const [contents, setContents] = useState(<></>)

    const isInvalidStock = useRef(true)

    const onStockRequest = async () => {
        setLoading(true)
        //API call to get the stock data based on the ticker input
        const resp = await fetch("api/baraggregate/ticker/" + ticker)

        //Check if the stock is valid
        await resp.json().then(async value => {
            console.log(value)
            await setStock(value)

            //Check if the input is valid, otherwise return error
            isInvalidStock.current = (value === undefined || value.results == null);
        });

        //Check if the input is valid, otherwise return error
        await setLoading(false);
    };



    useEffect(() => {
        const updateContents = async function () {
            console.log("Updating contents");
            console.log("Is Invalid: " + isInvalidStock.current + " | Loading: " + loading)
            if (!loading) {
                console.log("WE IN")
                if (isInvalidStock.current) return (<p>INVALID STOCK INPUTTED!</p>)
                else 
                {
                    //Get stock data and then format data to be passed to the chart
                    let day = new Date();
                    day.setDate(day.getDate() + 1)

                    let dataToGraph = []

                    for (const result of stock.results)
                    {
                        let newDay = new Date(day)
                        newDay.setDate(day.getDate() - 1)
                        dataToGraph.push({ x: newDay, y: [result.o, result.h, result.l, result.c] })

                        day = newDay
                        console.log(day)
                    }
                    console.log(dataToGraph)

                    let series = [{data: dataToGraph}]
                    return (
                        <>
                            <ApexCandleStick series={series} name={ticker} />
                            <p>{stock.ticker} -> STOCK FOUND! -> {stock.results[0].c}</p>
                        </>)
                }
            }
            else return (<p>Loading Data...</p>)
        }
        updateContents().then(value => {
            console.log(value)
            setContents(value)
        })
    }, [loading])


    return (
        <>
            {contents}
            <button onClick={() => onStockRequest()}>Get monthly stock data</button>
            <input id="ticker-input" width="500" value={ticker} onChange={e => setTicker(e.target.value)}></input>
        </>
    );
}