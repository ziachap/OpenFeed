import * as React from "react";
import * as NewsReelState from "../store/NewsReel";
import { NewsReelProps } from "./NewsReel";
import { ApplicationState } from "../store";
import { connect } from "react-redux";

export class SearchInput extends React.Component<NewsReelProps, {}> {

	constructor(props: NewsReelProps) {
		super(props);
	}

	setTextHandler(event: any): void {
		const value = event.target.value;
		this.props.setText(value);
		this.props.requestArticles();
	}

	clearTextHandler(event: any): void {
		this.props.setText("");
		this.props.requestArticles();
	}

	render() {
		return <div>
			       <div className="ui input">
				       <input type="text" placeholder="Search..." value={this.props.searchConfiguration.text}
				              onChange={(e: any) => this.setTextHandler(e)}/>
			       </div>
			       {this.defined(this.props.searchConfiguration.text) ? this.renderClearButton() : ""}

		       </div>;
	}

	private renderClearButton() {
		return <a className="ui tiny item" onClick={(e: any) => this.clearTextHandler(e)}>CLEAR</a>;
	}


	private defined(value: any): boolean {
		return value !== undefined && value !== null && value !== "";
	}
}

export default connect(
	(state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
	NewsReelState.actionCreators // Selects which action creators are merged into the component's props
)(SearchInput) as typeof SearchInput