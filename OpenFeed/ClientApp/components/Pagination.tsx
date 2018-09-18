import * as React from "react";
import * as NewsReelState from "../store/NewsReel";
import { NewsReelProps } from "./NewsReel";
import { ApplicationState } from "../store";
import { connect } from "react-redux";

export class Pagination extends React.Component<NewsReelProps, {}> {

	constructor(props: NewsReelProps) {
		super(props);
	}

	setPageHandler(event: any, page: number): void {
		this.props.setPage(page);
		this.props.requestArticles();
	}

	render() {
		return <div className="ui pagination menu">
			       {this.renderPaginationInput("<",
				       this.props.paginatedArticles.page - 1,
				       false,
				       !this.props.paginatedArticles.hasPreviousPage)}
			       {
				       Array.from(Array(Math.min(12, this.props.paginatedArticles.totalPages)).keys())
					       .map(i => this.renderPaginationInput(String(i + 1),
						       i,
						       this.props.paginatedArticles.page === i,
						       false))
			       }
			       {this.renderPaginationInput(">",
				       this.props.paginatedArticles.page + 1,
				       false,
				       !this.props.paginatedArticles.hasNextPage)}
		       </div>;
	}

	private renderPaginationInput(text: string, page: number, active: boolean, disabled: boolean) {
		return <a key={page} className={`item${active ? " active" : ""}${disabled ? " disabled" : ""}`}
		          onClick={(e: any) => disabled ? null : this.setPageHandler(e, page)}>
			       {text}
		       </a>;
	}
}

export default connect(
	(state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
	NewsReelState.actionCreators // Selects which action creators are merged into the component's props
)(Pagination) as typeof Pagination