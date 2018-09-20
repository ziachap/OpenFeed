import * as React from "react";
import * as NewsReelState from "../store/NewsReel";
import { NewsReelProps } from "./NewsReel";
import { ApplicationState } from "../store";
import { connect } from "react-redux";
import { sortTypes } from "../models/SortTypes";

export class Sort extends React.Component<NewsReelProps, {}> {

	constructor(props: NewsReelProps) {
		super(props);
	}

	setSortTypeHandler(event: any, sortType: number): void {
		this.props.setSortType(sortType);
		this.props.requestArticles();
	}

	render() {
		return <div><div className="header item">Sort By</div>
			{sortTypes.map(sortType => this.renderSortInput(sortType.name, sortType.id))}
		       </div>;
	}

	private renderSortInput(text: string, id: number) {
		return <a key={id} className={`item ${this.props.searchConfiguration.sortTypeId === id ? "active" : ""}`}
			onClick={(e: any) => this.setSortTypeHandler(e, id)}>
			       {text}
		       </a>;
	}
}

export default connect(
	(state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
	NewsReelState.actionCreators // Selects which action creators are merged into the component's props
)(Sort) as typeof Sort