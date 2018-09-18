import * as React from "react";
import * as NewsReelState from "../store/NewsReel";
import { NewsReelProps } from "./NewsReel";
import { ApplicationState } from "../store";
import { connect } from "react-redux";
import { newsCategories } from "../models/NewsCategories";

export class Categories extends React.Component<NewsReelProps, {}> {

	constructor(props: NewsReelProps) {
		super(props);
	}

	setCategoryHandler(event: any, category?: number): void {
		this.props.setCategory(category);
		this.props.requestArticles();
	}

	render() {
		return <div>
			       <div className="header item">Categories</div>
			       {this.renderCategoryInput("All", undefined)}
			       {newsCategories.map(category => this.renderCategoryInput(category.name, category.id))}
		       </div>;
	}

	private renderCategoryInput(text: string, id?: number) {
		return <a key={text} className={`item ${this.props.searchConfiguration.categoryId === id ? "active" : ""}`}
		          onClick={(e: any) => this.setCategoryHandler(e, id)}>
			       {text}
		       </a>;
	}
}

export default connect(
	(state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
	NewsReelState.actionCreators // Selects which action creators are merged into the component's props
)(Categories) as typeof Categories