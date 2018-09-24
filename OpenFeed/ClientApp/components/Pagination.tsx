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
		const totalPages = this.props.paginatedArticles.totalPages;
		return totalPages > 9 ? this.renderAbbreviatedPagination() : this.renderPagination();
	}

	private renderPagination() {
		const totalPages = this.props.paginatedArticles.totalPages;
		return <div className="ui small pagination menu">
			       {this.renderPaginationInput("<",
				       this.props.paginatedArticles.page - 1,
				       false,
				       !this.props.paginatedArticles.hasPreviousPage)}
			       {
				Array.from(Array(totalPages).keys())
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

	private renderAbbreviatedPagination() {
		const totalPages = this.props.paginatedArticles.totalPages;
		return <div className="ui small pagination menu">
			       {this.renderPaginationInput("<",
				       this.props.paginatedArticles.page - 1,
				       false,
				       !this.props.paginatedArticles.hasPreviousPage)}
			{this.renderPaginationInput(String(1), 0, this.props.paginatedArticles.page === 0, false)}
			{this.renderPaginationInput(String(2), 1, this.props.paginatedArticles.page === 1, false)}
			{this.renderStubPaginationInput()}
			       {
				this.getAbbreviatedCentralPaginationNumbers()
					       .map(i => this.renderPaginationInput(String(i + 1),
						       i,
						       this.props.paginatedArticles.page === i,
						       false))
			       }
			{this.renderStubPaginationInput()}
			{this.renderPaginationInput(String(totalPages - 1), totalPages - 2, this.props.paginatedArticles.page === totalPages - 2, false)}
			{this.renderPaginationInput(String(totalPages), totalPages - 1, this.props.paginatedArticles.page === totalPages - 1, false)}
			       {this.renderPaginationInput(">",
				       this.props.paginatedArticles.page + 1,
				       false,
				       !this.props.paginatedArticles.hasNextPage)}
		       </div>;
	}

	private renderPaginationInput(text: string, page: number, active: boolean, disabled: boolean) {
		return <a className={`item${active ? " active" : ""}${disabled ? " disabled" : ""}`}
		          onClick={(e: any) => disabled ? null : this.setPageHandler(e, page)}>
			       {text}
		       </a>;
	}

	private renderStubPaginationInput() {
		return <a className="item disabled">...</a>;
	}

	private getAbbreviatedCentralPaginationNumbers(): number[] {
		const pageTrim = 2;
		const centerWidth = 5;
		const centerWidthOffset = (centerWidth - 1)/2;

		const page = this.props.paginatedArticles.page;
		const totalPages = this.props.paginatedArticles.totalPages;
		const minStartPageIndex = pageTrim;
		const minEndPageIndex = minStartPageIndex + (centerWidth - 1);
		const maxEndPageIndex = totalPages - pageTrim - 1;
		const maxStartPageIndex = maxEndPageIndex - (centerWidth - 1);
		
		const startPageIndex = Math.max(Math.min(maxStartPageIndex, page - centerWidthOffset), minStartPageIndex);
		const endPageIndex = Math.max(Math.min(maxEndPageIndex, page + centerWidthOffset), minEndPageIndex);
		
		const pageSet = new Array();
		for (let i = startPageIndex; i <= endPageIndex; i++) {
			pageSet.push(i);
		}
		return pageSet;
	}
}

export default connect(
	(state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
	NewsReelState.actionCreators // Selects which action creators are merged into the component's props
)(Pagination) as typeof Pagination