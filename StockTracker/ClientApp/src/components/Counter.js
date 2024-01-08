import React, { Component } from 'react';
import { Route } from 'react-router';
import { CurrentStockData } from './CurrentStockData'

export class Counter extends Component {

    static displayName = Counter.name;

    constructor(props) {
        super(props);
        this.state = { ticker: "change this!", currentCount: 0 };
        this.incrementCounter = this.incrementCounter.bind(this);
    }

    incrementCounter() {
        this.setState({
            ticker: 'bruh'
        });
    }

    render() {

        return (
            <div>
                <h1>Counter</h1>

                <p aria-live="assertive">This is a simple example of a React component. {this.state.ticker}</p>

                <p aria-live="polite">Current count: <strong>{this.state.ticker}</strong></p>

                <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
                <CurrentStockData />
            </div>
        );
    }

    async getStockData() {
        const resp = await fetch("api/baraggregate")
        this.setState({ ticker: resp })
    }
}
